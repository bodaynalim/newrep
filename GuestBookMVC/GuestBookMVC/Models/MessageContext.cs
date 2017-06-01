using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GuestBookMVC.Models
{
    public class MessageContext : IdentityDbContext
    {
       
        public DbSet<Message> Messages{ get; set; }


        public MessageContext(DbContextOptions<MessageContext> options)
            : base(options)
        {
            
        }
    }
}
