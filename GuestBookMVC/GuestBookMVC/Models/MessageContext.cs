using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBookMVC.Models
{
    public class MessageContext : DbContext
    {
      
        public DbSet<Phones> Phone { get; set; }
       

        public MessageContext(DbContextOptions<MessageContext> options)
            : base(options)
        {
            
        }
    }
}
