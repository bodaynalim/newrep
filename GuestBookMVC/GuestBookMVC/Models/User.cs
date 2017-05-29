using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBookMVC.Models
{
    public class User
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsLoginnig { get; set; }


        public User()
        {
            
        }
        public User (string login, string password, bool admin = false)
        {
            IsAdmin = admin;
            Login = login;
            Password = password;

        }
        
    }
}
