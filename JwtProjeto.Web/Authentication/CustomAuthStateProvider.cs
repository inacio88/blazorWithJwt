using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtProjeto.Models.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace JwtProjeto.Web.Authentication
{
    public class CustomAuthStateProvider(ProtectedLocalStorage protectedLocalStorage) : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var sessionModel = (await protectedLocalStorage.GetAsync<LoginResponseModel>("sessionState")).Value;
            var identity = sessionModel == null ? new ClaimsIdentity() : GetClaimsIdentity(sessionModel.Token);
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public async Task MarkUserAsAuthenticated(LoginResponseModel model)
        {
            await protectedLocalStorage.SetAsync("sessionState", model);
            var identity = GetClaimsIdentity(model.Token);
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity GetClaimsIdentity(string token)
        {
            var hander = new JwtSecurityTokenHandler();
            var jwtToken = hander.ReadJwtToken(token);
            var claims = jwtToken.Claims;
            return new ClaimsIdentity(claims, "jwt");
        }
         
    }
}