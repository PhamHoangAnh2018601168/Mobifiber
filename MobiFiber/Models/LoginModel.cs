using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Micro.Web.Models
{
    public class LoginModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Input User Name")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Input Password")]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
