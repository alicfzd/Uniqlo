﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Uniqloo.Models;
using Uniqloo.ViewModel.Auths;

namespace Uniqloo.Controllers
{
    public class AccountControlle(UserManager<User> _userManager) : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid) 
            { 
                return View();
            }
            User user = new User()
            {
                Fullname = vm.Username,
                Email = vm.Email,
                UserName = vm.Username
            };
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors) 
                {
                    ModelState.AddModelError("",err.Description);
                }
            }
            if (ModelState.IsValid)
            {
                return View();
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
