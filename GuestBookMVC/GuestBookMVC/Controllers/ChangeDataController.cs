using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GuestBookMVC.Models;
using GuestBookMVC.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace GuestBookMVC.Controllers
{
    public class ChangeDataController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        IHostingEnvironment _appEnvironment;
        private UserContext db;

        public ChangeDataController(UserManager<User> userManager, SignInManager<User> signInManager, UserContext con, IHostingEnvironment appEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = con;
            _appEnvironment = appEnvironment;
        }

        public IActionResult EditPersonaldata()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangeUserDataViewModel model, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                var userId = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name).Id;
                User user = await _userManager.FindByIdAsync(userId);
                if (!string.IsNullOrEmpty(model.Town))
                {
                    user.Town = model.Town;
                }

                if (uploadedFile != null && uploadedFile.Length < 10000000 && (uploadedFile.ContentType == "image/jpeg"
                                                                               || uploadedFile.ContentType == "image/png"))
                {
                    string FileName = Guid.NewGuid() + uploadedFile.FileName;
                    string path = "/Pictures/" + FileName;
                   
                    
                   
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }

                    FileModel file = new FileModel
                    {
                        Name = FileName,
                        Path = path,
                    };

                    db.Picture.Add(file);

                   user.FileId = file.Id;


                }

                if (user != null)
                {
                    IdentityResult result2 = await _userManager.UpdateAsync(user);
                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                   

                    if (result.Succeeded && result2.Succeeded)
                    { 
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }
    }
}
