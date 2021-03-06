using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.WPF.Client
{
    public class SignUpViewModel
    {
        [Required]
        //[Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        //[Display(Name = "Email")]
        public string Email { get; set; }

        //[Required]
        //[Display(Name = "Год рождения")]
        //public int Year { get; set; }

        [Required]
        [DataType(DataType.Password)]
        //[Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        [DataType(DataType.Password)]
        //[Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
