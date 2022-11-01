using MarkdownApp.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;

namespace MarkdownApp.Shared.Authentification;

public class MarkDownAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    public MarkDownAuthenticationStateProvider(HttpClient httpclient)
    {
        _httpClient = httpclient;
    }

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        User? currenUser = await _httpClient.GetFromJsonAsync<User>("user/getcurrentuser");
        if (currenUser != null && currenUser.Email != null)
        {
            var identity = new ClaimsIdentity(new[]{
                        new Claim(ClaimTypes.Name, currenUser.FirstName)
                        ,  new Claim(ClaimTypes.Email, currenUser.Email)
                        }, "Authenticated");

            var claimsPrincipal = new ClaimsPrincipal(identity);

            return new AuthenticationState(claimsPrincipal);
        }
        else
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }
}

