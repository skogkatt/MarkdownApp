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
        User currenUser = await _httpClient.GetFromJsonAsync<User>("user/getcurrentuser");
        if (currenUser != null && currenUser.Email != null)
        {
            var identity = new ClaimsIdentity(new[]{
                        new Claim(ClaimTypes.Name, currenUser.Email)
                        }, "Authenticated");

            var claimsPrincipal = new ClaimsPrincipal(identity);

            return new AuthenticationState(claimsPrincipal);
        }

        else
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }
    //private IMemoryCache _memoryCache { get; set; }

    //public override Task<AuthenticationState> GetAuthenticationStateAsync()
    //{
    //    //_memoryCache.TryGetValue("claimsPrincipal", out ClaimsPrincipal claimsPrincipal);
    //    if (claimsPrincipal == null)
    //        claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());

    //    return Task.FromResult(new AuthenticationState(claimsPrincipal));
    //}
    //public void IsAuthenticated(User currentUser)
    //{
    //    var identity = new ClaimsIdentity(new[]{
    //        new Claim(ClaimTypes.Name, currentUser.Email)
    //        }, "Authenticated");

    //    var user = new ClaimsPrincipal(identity);
    //    _memoryCache.Set("claimsPrincipal", user);
    //    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    //}

}

