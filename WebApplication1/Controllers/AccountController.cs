using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public ViewResult Register()
        {
            return View();
        }
        
        [AllowAnonymous]
        [AcceptVerbs("GET","POST")]
        public async Task<IActionResult> IsUserNameAvailable(string UserName)
        {
            var Result = await userManager.FindByNameAsync(UserName).ConfigureAwait(true);
            if (Result is null)
            {
            return Json(true);
            }
            else
            {
                return Json($"The User Name {UserName} is already in use");
            }
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(LoginViewModel register)
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

            return RedirectToAction(nameof(HomeController.Index),nameof(HomeController)[0..^10]);
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel login)
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

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController)[0..^10]);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController)[0..^10]);
        }
    }
}