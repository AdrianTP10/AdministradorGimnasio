﻿using GymManager.Core.Members;
using GymManager.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager) 
        { 
            _userManager = userManager;
            _signInManager = signInManager;
            if (!_userManager.Users.Any())
            {
                var result = _userManager.CreateAsync(new IdentityUser{  Email = "adriantp098@gmail.com",
                    EmailConfirmed = true,
                    UserName = "adriantp098@gmail.com"
                }, "Pa$$word.1").Result;
            }
        }
        public IActionResult Login()
        {
            return View();
        }

        

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            string returnUrl = string.IsNullOrEmpty(Request.Query["returnUrl"]) ? Url.Content("~/") : Request.Query["returnUrl"];
            

            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email,model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                if(result.IsLockedOut)
                {
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                    return View();
                }
            }

            return View();
        }
    }
}
