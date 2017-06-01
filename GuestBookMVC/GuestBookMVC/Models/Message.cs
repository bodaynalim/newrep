using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GuestBookMVC.Models
{
    public class Message
    {
        public int Id { get; set; }

      
        [Required]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Длина строки должна быть от 10 до 50 символов")]
        public string MessageEmail { get; set; }

        [Required]
        public string Date { get; set; }

        public string IdentityUserId { get; set; }

        public IdentityUser User { get; set; }

    }
}
