using Azure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using WebApplication7.Data.Interfaces;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        public AccountController(UserManager<ApplicationUser> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }
        public IActionResult Login(string returnUrl)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(HomePageViewModel model)
        {
            var user = _userRepository.GetUser(model.Email);

            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);
                if (passwordCheck)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, "Author"),
                    new Claim("CustomProperty", "3")
                };
                    var identity = new ClaimsIdentity(claims, "AuthCookie");
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.AuthenticateAsync("AuthCookie");
                    await HttpContext.SignInAsync("AuthCookie", principal);
                    var username = HttpContext.User.Identity.Name;
                }
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

            var newUser = new ApplicationUser { Email = model.Email, UserName = model.UserName };
            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {

            }
            return RedirectToAction("Index", "Home");


        }
    }
}
