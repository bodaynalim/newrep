using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GuestBookMVC.Models;
using GuestBookMVC.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GuestBookMVC.Controllers
{
    public class HomeController : Controller
    {
        UserContext db;
        
        private IMessageEmail em;
        IHostingEnvironment _appEnvironment;

        public HomeController(UserContext context, IMessageEmail email, IHostingEnvironment appEnvironment)
        {
            db = context;
            em = email;

            _appEnvironment = appEnvironment;
        }

      
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["UserId"] = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name).Id;
                
           return View(db.Messages.ToList());
        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {

            if (id != null)
            {
                Message phone = db.Messages.FirstOrDefault(p => p.Id == id);
                if (phone != null)
                {
                    db.Messages.Remove(phone);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(db.Messages.ToList());
        }

        

        [HttpPost]
        public async Task<IActionResult> Index(Message message, IFormFile uploadedFile)
        {
            message.UserId = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name).Id;
            if (ModelState.IsValid)
            {

                // Response.Cookies.Append(message.Name, message.MessageEmail);
                //   em.Send(message.Email);

                message.FileId = null;

            if (uploadedFile != null && uploadedFile.Length < 10000000 && (uploadedFile.ContentType== "image/jpeg"
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

                message.FileId = file.Id;

            }
                db.Messages.Add(message);
                db.SaveChanges();
            }
          


            return RedirectToAction("Index");
        }

        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            
            
            return View(new Message(){Id = id,MessageEmail = db.Messages.FirstOrDefault(p => p.Id == id).MessageEmail });
        }

        [HttpGet]
        public IActionResult Download(int id)
        {
            string path = db.Picture.FirstOrDefault(x => x.Id == id).Path;
            string name = db.Picture.FirstOrDefault(x => x.Id == id).Name;
            var filepath = _appEnvironment.WebRootPath+path;
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            return File(fileBytes, "application/x-msdownload", name);
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult Edit(Message phone)
        {

            if (phone.Id != null)
            {
                Message phones = db.Messages.FirstOrDefault(p => p.Id == phone.Id);
                if (phones != null)
                {
                    db.Messages.FirstOrDefault(p => p.Id == phone.Id).MessageEmail = phone.MessageEmail;
                    db.SaveChanges();

                }
            }
            return RedirectToAction("Index");
        }

    }
}
