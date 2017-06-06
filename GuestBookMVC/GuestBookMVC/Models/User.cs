using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GuestBookMVC.Models
{
    public class User : IdentityUser
    {
       public string Town { get; set; }

        public int? FileId { get; set; }

        public FileModel File { get; set; }
    }
}
