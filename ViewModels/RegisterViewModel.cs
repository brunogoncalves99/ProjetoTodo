using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Prompt = "Name")]
        public String Name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Prompt = "Email")]
        public String Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Prompt = "Password")]
        public String Password { get; set; }
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Prompt = "Password Confirmation")]
        public String PasswordConfirmation { get; set; }
    }
}