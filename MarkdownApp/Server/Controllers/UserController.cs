using MarkdownApp.Server.Data;
using MarkdownApp.Shared;
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
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly MarkdownContext _context;

        public UserController(ILogger<UserController> logger, MarkdownContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("getcurrentuser")]
        public async Task<ActionResult<User>> GetAsync()
        {
            try
            {
                User currentUser = new User();

                if(User.Identity.IsAuthenticated)
                {
                    currentUser.FirstName = User.FindFirstValue(ClaimTypes.Name);
                    currentUser.Email = User.FindFirstValue(ClaimTypes.Email);
                }

                return await Task.FromResult(currentUser);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred");
            }

        }

        [HttpPost("loginuser")]
        public async Task<ActionResult<string>> LoginUser(User inputCredentials)
        {
            try
            {
                User loggedUser = _context.Users.Where(u => u.Email == inputCredentials.Email && u.Password == inputCredentials.Password).FirstOrDefault();

                if (loggedUser != null)
                {
                    if (loggedUser.Email.Equals(inputCredentials.Email) && loggedUser.Password.Equals(inputCredentials.Password))
                    {
                        var identity = new ClaimsIdentity(new[]{
                        new Claim(ClaimTypes.Name, loggedUser.FirstName)
                        ,  new Claim(ClaimTypes.Email, loggedUser.Email)
                        }, "Authenticated");

                        var user = new ClaimsPrincipal(identity);

                        //Sing In User
                        await HttpContext.SignInAsync(user);

                        return string.Empty;
                    }
                }

                return "Invalid credentials";
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred");
            }
        }

        [HttpGet("logoutuser")]
        public async Task<ActionResult<string>> LogOutUser()
        {
            try
            {
                await HttpContext.SignOutAsync();
                return "Success";
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred");
            }
        }
    }
}