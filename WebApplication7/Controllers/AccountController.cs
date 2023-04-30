using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(string returnUrl)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(HomePageViewModel model)
        {
            if (model.Username == "test")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Name1"),
                    new Claim(ClaimTypes.Role, "Author"),
                    new Claim("CustomProperty", "3")
                };
                var identity = new ClaimsIdentity(claims, "AuthCookie");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("AuthCookie", principal);

            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("AuthCookie");

            return RedirectToAction("Index", "Home");
        }
    }
}
