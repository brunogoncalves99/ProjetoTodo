using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Prompt = "Email")]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Prompt = "Password")]
        public String Password { get; set; }
    }
}