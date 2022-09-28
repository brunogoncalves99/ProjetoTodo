using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo.ViewModels
{
    public class ItemViewModel
    {
        [Required]
        [Display(Prompt = "Description")]
        public String Description { get; set; }
    }
}