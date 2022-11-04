using MarkdownApp.Shared.Models;
using MarkdownApp.Shared.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace MarkdownApp.Shared.ViewModels
{
    public class MarkDownViewModel : IMarkDownViewModel
    {
        public string Markdown { get; set; }

        public readonly HttpClient _httpClient;

        public MarkDownViewModel()
        {
        }

        public MarkDownViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GenerateHtmlFromMarkdownAsync(string paramContent)
        {
            if (!string.IsNullOrEmpty(paramContent))
            {
                using (HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"document/generatemarkdown?content={paramContent}", ""))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (!String.IsNullOrEmpty(apiResponse))
                    {
                        return apiResponse;
                    }
                }
            }

            return string.Empty;
        }

        public async Task<string> GetMarkdownFromDocumentAsync()
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync($"document/getmarkdown"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!String.IsNullOrEmpty(apiResponse))
                {
                    return apiResponse;
                }
            }
            return string.Empty;
        }
    }
}
