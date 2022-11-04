using MarkdownApp.Client;
using MarkdownApp.Shared.Authentification;
using MarkdownApp.Shared.Models;
using MarkdownApp.Shared.ViewModels;
using MarkdownApp.Shared.ViewModels.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ILoginViewModel, LoginViewModel>();
builder.Services.AddScoped<IMarkDownViewModel, MarkDownViewModel>();
builder.Services.AddScoped<AuthenticationStateProvider, MarkDownAuthenticationStateProvider>();

builder.Services.AddSingleton<User>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
