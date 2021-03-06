﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuestBookMVC.Models;
using GuestBookMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace GuestBookMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [ActionName("Reg")]
        public async Task<IActionResult> Reg(RegistrationViewModel user)
        {
            if (ModelState.IsValid)
            {
               User userreg = new User { Email = user.Email, UserName = user.Email, Town = user.Town, FileId = null};
                // добавляем пользователя
                var result = await _userManager.CreateAsync(userreg, user.Password);
                if (result.Succeeded)
                {
                    // установка куки
                   // await _signInManager.SignInAsync(userreg, false);
                    return RedirectToAction("LIndex", "Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(user);
        }
        public IActionResult LIndex()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("LIndex");
        }

        [HttpGet]
        public IActionResult Reg()
        {
            return View("Reg");
        }


   
        [HttpPost]
        [ActionName("LIndex")]
        public async Task<IActionResult> LIndex(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(user.Email, user.Password,false,false);
                if (result.Succeeded)
                {
                   return RedirectToAction("Index", "Home");
                    
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(user);
            
        }

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("LIndex", "Login");
        }
    }
}
