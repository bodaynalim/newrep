using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuestBookMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuestBookMVC.Controllers
{
    public class LoginController : Controller
    {
        private static readonly User[] Users = new User[2]{ new User( "Bogdan", "Nalyvaiko", true), new User("Bogdan2", "Nalyvaiko", false) };

        public IActionResult LIndex()
        {
            return View();
        }

        public static bool isAdmin;
        public static bool isLogin;
        [HttpPost]
        [ActionName("Redirect")]
        public IActionResult Redirect(User user)
        {
            if (user.Login == Users[0].Login && user.Password == Users[0].Password)
            {
                isAdmin = Users[0].IsAdmin;
                isLogin = Users[0].IsLoginnig = true;
                return Redirect("~/Home/Index");

            }
            if (user.Login == Users[1].Login && user.Password == Users[1].Password)
            {
                isAdmin = Users[1].IsAdmin;

                isLogin = Users[1].IsLoginnig = true;
                return Redirect("~/Home/Index");
            }

            return View("LIndex");
        }
    }
}
