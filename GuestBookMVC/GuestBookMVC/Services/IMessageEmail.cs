using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBookMVC.Services
{
    public interface IMessageEmail
    {
        void Send(string email);
    }
}
