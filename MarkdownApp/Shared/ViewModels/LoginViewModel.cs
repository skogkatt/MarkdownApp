using MarkdownApp.Shared.Models;
using MarkdownApp.Shared.Services.Interfaces;
using MarkdownApp.Shared.ViewModels.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

#nullable disable


namespace MarkdownApp.Shared.ViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        private readonly HttpClient _httpClient;

        public LoginViewModel()
        {
        }
        public LoginViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> LoginUser(User credentials)
        {
            string errorMessage = string.Empty;
            using (HttpResponseMessage response = await _httpClient.PostAsJsonAsync<User>("user/loginuser", credentials))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!String.IsNullOrEmpty(apiResponse))
                {
                    errorMessage = apiResponse;
                }
            }

            return errorMessage;
        }

    }
}
