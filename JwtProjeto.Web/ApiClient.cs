using System.Net.Http.Headers;
using System.Text.Json;
using JwtProjeto.Models.Models;
using JwtProjeto.Web.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;

namespace JwtProjeto.Web;

public class ApiClient(HttpClient httpClient, ProtectedLocalStorage protectedLocalStorage, AuthenticationStateProvider authenticationStateProvider)
{
    public async Task SetAuthorizeHeader()
    {
        var sessionState = (await protectedLocalStorage.GetAsync<LoginResponseModel>("sessionState")).Value;
        if (sessionState is not null && !string.IsNullOrEmpty(sessionState.Token))
        {
            if (sessionState.TokenExpired < DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            {
                await ((CustomAuthStateProvider)authenticationStateProvider).MarkUserAsLoggedOut();
            }
            else if (sessionState.TokenExpired < DateTimeOffset.UtcNow.AddMinutes(10).ToUnixTimeSeconds())
            {
                var res = await httpClient.GetFromJsonAsync<LoginResponseModel>($"/api/auth/loginByRefreshToken?refreshToken={sessionState.RefreshToken}");
                if (res is not null)
                {
                    await ((CustomAuthStateProvider)authenticationStateProvider).MarkUserAsAuthenticated(res);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", res.Token);
                }
                else
                {
                    await ((CustomAuthStateProvider)authenticationStateProvider).MarkUserAsLoggedOut();
                }
            }
            else
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionState.Token);
            }

        }
    }
    public async Task<T> GetFromJsonAsync<T>(string path)
    {
        await SetAuthorizeHeader();
        return await httpClient.GetFromJsonAsync<T>(path);
    }

    public async Task<T1> PostAsync<T1, T2>(string path, T2 postModel)
    {
        await SetAuthorizeHeader();

        var res = await httpClient.PostAsJsonAsync(path, postModel);
        if (res is not null && res.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<T1>(await res.Content.ReadAsStringAsync());
        }
        return default;
    }

    public async Task<T1> PutAsync<T1, T2>(string path, T2 putModel)
    {
        await SetAuthorizeHeader();

        var res = await httpClient.PutAsJsonAsync(path, putModel);
        if (res is not null && res.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<T1>(await res.Content.ReadAsStringAsync());
        }
        return default;
    }

    public async Task<T> DeleteAsync<T>(string path)
    {
        await SetAuthorizeHeader();

        return await httpClient.DeleteFromJsonAsync<T>(path);
    }
}

