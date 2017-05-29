using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GuestBookMVC.Models;
using GuestBookMVC.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GuestBookMVC.Controllers
{
    public class HomeController : Controller
    {
        MessageContext db;
        private IMessageEmail em;

        public HomeController(MessageContext context, IMessageEmail email)
        {
            db = context;
            em = email;
        }
        public IActionResult Index()
        {
            ViewBag.id = LoginController.isAdmin;
            ViewBag.isLog = LoginController.isLogin;
            return View(db.Phone.ToList());
        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {

            if (id != null)
            {
                Phones phone = db.Phone.FirstOrDefault(p => p.Id == id);
                if (phone != null)
                {
                    db.Phone.Remove(phone);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(db.Phone.ToList());
        }

        

        [HttpPost]
        public IActionResult Index(Phones message)
        {
            
            if (ModelState.IsValid)
            {
                if (db.Phone.Any(x=>x.Name == message.Name && x.Email == message.Email && x.MessageEmail == message.MessageEmail))
                {
                }
                else
                {
                    if (message.MessageEmail.Contains("\n"))
                    {
                        //   message.MessageEmail.Replace("\n", "</br>"); 
                    }
                    db.Phone.Add(message);
                    Response.Cookies.Append(message.Name, message.MessageEmail);
                    db.SaveChanges();

                  em.Send(message.Email);
                }
                
            }


            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            
            
            return View(new Phones(){Id = id,MessageEmail = db.Phone.FirstOrDefault(p => p.Id == id).MessageEmail });
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult Edit(Phones phone)
        {

            if (phone.Id != null)
            {
                Phones phones = db.Phone.FirstOrDefault(p => p.Id == phone.Id);
                if (phones != null)
                {
                    db.Phone.FirstOrDefault(p => p.Id == phone.Id).MessageEmail = phone.MessageEmail;
                    db.SaveChanges();

                }
            }
            return RedirectToAction("Index");
        }

    }
}
