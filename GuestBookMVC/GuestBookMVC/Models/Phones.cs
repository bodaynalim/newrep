using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBookMVC.Models
{
    public class Phones 
    {
      
        public int Id { get; set; }

        [Required ]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 20 символов")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Длина строки должна быть от 10 до 50 символов")]
        public string MessageEmail { get; set; }

        [Required]
        public string Date { get; set; }

    }
}
