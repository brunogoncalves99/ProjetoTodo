using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo.ViewModels
{
    public class ProfileViewModel
    {
        [Required]
        public String Name { get; set; }

        [EmailAddress]
        public String Email { get; set; }

        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password Confirmation")]
        public String PasswordConfirmation { get; set; }
    }
}