using MarkdownApp.Server.Data;
using MarkdownApp.Shared.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
#nullable disable

namespace MarkdownApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly ILogger<DocumentController> _logger;
        private readonly MarkdownContext _context;

        public DocumentController(ILogger<DocumentController> logger, MarkdownContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost("generatemarkdown")]
        public async Task<ActionResult<string>> TextToHtmlAsync(string content)
        {
            try
            {
                string htmlResult = Markdig.Markdown.ToHtml(content);

                return await Task.FromResult(htmlResult);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred");
            }

        }

        [HttpGet("getmarkdown")]
        public async Task<ActionResult<string>> GetMarkDownAsync()
        {
            try
            {
                Document document = _context.Document.FirstOrDefault();
                
                if (document != null)
                {
                    return await Task.FromResult(document.Markdown);
                }

                return await Task.FromResult("Document not found");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred");
            }

        }
    }
}