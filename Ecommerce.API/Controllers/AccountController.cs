using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
  
    public class AccountController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AccountController(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

       
        [HttpGet("login")]
        public IActionResult Login()
        {

            var props = new AuthenticationProperties { RedirectUri = "https://localhost:7256" };
            return Challenge(props, GoogleDefaults.AuthenticationScheme);
        }
        
        [HttpGet("signin-google")]
        public async Task<IActionResult> GoogleLogin()
        {
            var response = await httpContextAccessor.HttpContext!.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (response.Principal == null) return BadRequest();

          
            return Ok();
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/swagger/index.html"); // Redirect to your home page or desired logout page
        }
    }

}
