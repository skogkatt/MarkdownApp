using MarkdownApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownApp.Shared.ViewModels.Interfaces
{
    public interface IMarkDownViewModel
    {
        public string Markdown { get; set; }
        public Task<string> GenerateHtmlFromMarkdownAsync(string document);
        public Task<string> GetMarkdownFromDocumentAsync();
    }
}
