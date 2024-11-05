using System.Text.Json;
using JwtProjeto.Models.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;

namespace JwtProjeto.Web;

public class ApiClient(HttpClient httpClient, ProtectedLocalStorage protectedLocalStorage)
{
    public async Task SetAuthorizeHeader()
    {
        var token = (await protectedLocalStorage.GetAsync<LoginResponseModel>("sessionState")).Value;
        if (token is not null)
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);
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

