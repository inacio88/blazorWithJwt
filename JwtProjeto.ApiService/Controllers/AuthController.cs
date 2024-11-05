using System.Security.Claims;
using JwtProjeto.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Identity.Client;

namespace JwtProjeto.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IConfiguration configuration) : ControllerBase
    {
        [HttpPost("login")]
        public ActionResult<LoginResponseModel> Login(LoginModel login)
        {
            if (login.UserName == "Admin" && login.Password == "Admin")
            {
                var token = GenerateJwtToken(login.UserName);
                return Ok(new LoginResponseModel{Token = token});
            }
            return null;
        }

        private string GenerateJwtToken(string userName)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, "Admin")
            };

            string secret = configuration.GetValue<string>("Jwt:Secret");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "inacio",
                audience: "inacio",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}