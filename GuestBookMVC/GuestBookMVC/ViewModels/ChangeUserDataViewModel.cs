using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GuestBookMVC.ViewModels
{
    public class ChangeUserDataViewModel
    {
        public string Email { get; set; }

        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        
        public string Town { get; set; }
    }
}
