using System.Security.Claims;
using JwtProjeto.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using JwtProjeto.BL.Services;
using JwtProjeto.Models.Entities;

namespace JwtProjeto.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IConfiguration configuration, IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginModel login)
        {
            var user = await authService.GetUserByLogin(login.UserName, login.Password);
            if (user is not null)
            {
                var token = GenerateJwtToken(user, isRefreshToken: false);
                var refreshToken = GenerateJwtToken(user, isRefreshToken: true);

                await authService.AddRefreshTokenModel(new RefreshTokenModel
                {
                    RefreshToken = refreshToken,
                    UserId = user.Id
                });

                return Ok(new LoginResponseModel
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    TokenExpired = DateTimeOffset.UtcNow.AddMinutes(8).ToUnixTimeSeconds()
                });
            }
            return null;
        }

        [HttpGet("loginByRefreshToken")]
        public async Task<ActionResult<LoginResponseModel>> LoginByRefreshToken(string refreshToken)
        {
            var refreshTokenModel = await authService.GetRefreshTokenModel(refreshToken);
            if (refreshTokenModel is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var newToken = GenerateJwtToken(refreshTokenModel.User, isRefreshToken: false);
            var newRefreshToken = GenerateJwtToken(refreshTokenModel.User, isRefreshToken: true);

            await authService.AddRefreshTokenModel(new RefreshTokenModel{
                RefreshToken = newRefreshToken,
                UserId = refreshTokenModel.UserId
            });

            return new LoginResponseModel
            {
                Token = newToken,
                TokenExpired = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds(),
                RefreshToken = newRefreshToken
            };

        }

        private string GenerateJwtToken(UserModel user, bool isRefreshToken)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
            };
            claims.AddRange(user.UserRoles.Select(n => new Claim(ClaimTypes.Name, n.Role.RoleName)));
            string secret = configuration.GetValue<string>($"Jwt:{(isRefreshToken ? "RefreshToken" : "Secret")}");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "inacio",
                audience: "inacio",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(isRefreshToken ? 24 * 60 : 9),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}