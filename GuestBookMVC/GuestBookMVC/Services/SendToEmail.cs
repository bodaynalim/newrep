using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace GuestBookMVC.Services
{
    public class SendToEmail : IMessageEmail
    {
        public  async void Send(string email)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Администрация сайта", "boday.nalim@gmail.com"));
            message.To.Add(new MailboxAddress("b.nalyvaiko@pervolo.com")); // кому отправлять  
            message.Subject = "Тема"; // тема письма  
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "Спасибо за ваше сообщение"
            };



            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587);
                await client.AuthenticateAsync("boday.nalim@gmail.com", "BogdanNalim1995");
                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }
        }
    }
}
