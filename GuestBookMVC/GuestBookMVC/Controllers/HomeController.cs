using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GuestBookMVC.Models;
using GuestBookMVC.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GuestBookMVC.Controllers
{
    public class HomeController : Controller
    {
        UserContext db;
        
        private IMessageEmail em;

        public HomeController(UserContext context, IMessageEmail email)
        {
            db = context;
            em = email;
           
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
        public IActionResult Index(Message message)
        {
            message.IdentityUserId = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name).Id;
            if (ModelState.IsValid)
            {
                    db.Messages.Add(message);
                   // Response.Cookies.Append(message.Name, message.MessageEmail);
                    db.SaveChanges();

                    return Index(); 
                //   em.Send(message.Email);
                
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            
            
            return View(new Message(){Id = id,MessageEmail = db.Messages.FirstOrDefault(p => p.Id == id).MessageEmail });
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
