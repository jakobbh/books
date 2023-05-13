using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
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
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                TempData["Error"] = "This email is already registered";
                return View(model);
            }

            var newUser = new ApplicationUser { Email = model.Email };
            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {

            }
            return RedirectToAction("Index", "Home");


        }
    }
}
