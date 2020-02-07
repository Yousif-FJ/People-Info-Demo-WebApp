using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.ViewModule;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager , SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountDetail register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            IdentityUser user = new IdentityUser(register.UserName);

            var result = await userManager.CreateAsync(user,register.Password)
                .ConfigureAwait(true);

            if (!result.Succeeded) 
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                }
                return View(register);
            }
            await signInManager.SignInAsync(user,register.PresistantLogin)
                .ConfigureAwait(true);

            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountDetail login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var result = await signInManager.PasswordSignInAsync(userName: login.UserName,
                login.Password, 
                login.PresistantLogin,
                lockoutOnFailure: true)
                .ConfigureAwait(true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(login);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}