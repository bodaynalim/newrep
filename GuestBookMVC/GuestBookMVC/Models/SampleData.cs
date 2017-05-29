using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBookMVC.Models
{
    public class SampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService(typeof(MessageContext)) as MessageContext;
            
            if (!context.Phone.Any())
            {
                context.Phone.AddRange(
                    new Phones
                    {
                       
                        Name = "One",
                        Email = "boday1.nalim@gmail.com",
                        MessageEmail = "Hello 1"
                    },
                    new Phones
                    {
                        
                        Name = "Two",
                        Email = "boday2.nalim@gmail.com",
                        MessageEmail = "Hello 2"
                    },
                    new Phones
                    {
                       
                        Name = "Three",
                        Email = "boday3.nalim@gmail.com",
                        MessageEmail = "Hello 3"
                    }
                );

                context.SaveChanges(true);
            }
        }
    }
}
