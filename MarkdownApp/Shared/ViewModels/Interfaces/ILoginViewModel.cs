using MarkdownApp.Shared.Models;
namespace MarkdownApp.Shared.ViewModels.Interfaces
{
    public interface ILoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        Task<string> LoginUser(User credentials);
    }
}
