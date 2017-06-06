using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using GuestBookMVC.Controllers;
using GuestBookMVC.Models;
using GuestBookMVC.Services;
using GuestBookMVC.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.ComponentModel.DataAnnotations;

namespace XUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task ValidateAddinMessage()
        {
            using (var context = GetContextWithData())
            {
                var mock1 = new Mock<IMessageEmail>();

                var mock2 = new Mock<IHostingEnvironment>();
           
                var controller = new HomeController(context, mock1.Object, mock2.Object);
                Message mess = new Message()
                                            { MessageEmail = "DAAAskskwlwee", Id=4, Date = "adsd", UserId = "Apple12" };

                if (IsModelValid(mess))
                {
                    await controller.Index(mess, null);
                }

                // Assert
                Assert.Contains(mess, HomeController.db.Messages);
            }
          
        }

        [Fact]
        public void ValidateModelMessagek()
        {
            
                Message mess = new Message() { MessageEmail = "sdfpopopop", Id = 4, Date = "adsd", UserId = "Apple12" };
            
               // Assert
                Assert.True(IsModelValid(mess));
            

        }

        public bool IsModelValid(Message mess)
        {
            var valcontext = new ValidationContext(mess, null, null);
            var valresult = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(mess, valcontext, valresult, true);
            return valid;
        }

        [Fact]
        public void ValidateModelRegModel()
        {
           RegistrationViewModel mess = new RegistrationViewModel()
                                    { Email = "boday.nalim@gmail.com",
                                      Town ="Symu", Password = "hfg123N_", PasswordConfirm = "hfg123N_" };

                var valcon = new ValidationContext(mess, null, null);
                var valresult = new List<ValidationResult>();
                var valid = Validator.TryValidateObject(mess, valcon, valresult, true);

                // Assert
                Assert.True(valid);
           

        }

        [Fact]
        public void ValidateModelLoginModel()
        {
            LoginViewModel mess = new LoginViewModel()
            {
                Email = "boday.nalim@gmail.com",
                Password = "hfg123N_"
               
            };

            var valcon = new ValidationContext(mess, null, null);
            var valresult = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(mess, valcon, valresult, true);

            // Assert
            Assert.True(valid);


        }

        private List<Message> GetTestMessages()
        {
            var phones = new List<Message>()
            {
                new Message() { Id=1, MessageEmail= "iPhone 7544rree", UserId= "Apple12"},
                new Message() { Id=2, MessageEmail= "iPhone 7544rree", UserId= "Apple12"},
                new Message() { Id=3, MessageEmail= "iPhone 7544rree", UserId= "Apple12"}
            };

            
            return phones;
        }
        private List<User> GetTestUser()
        {
            var phones = new List<User>()
            {
                new User() { Id="Apple12",Email = "boday.nalim@gmail.com",PasswordHash ="AQAAAAEAACcQAAAAEKfbM3IsF9crbBhxvtheNcjOc/mQmoavyCGTxEfgAgLjm/FYQ6UokCpjkoAkOKuR6A==", Town = "Symu"},
                
            };
            return phones;
        }
        private UserContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<UserContext>().UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new UserContext(options);

            
            context.Messages.AddRange(GetTestMessages());
            context.Users.AddRange(GetTestUser());
            context.SaveChanges();

            return context;
        }
    }
}
