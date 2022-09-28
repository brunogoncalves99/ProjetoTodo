using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required]
        public DateTime CreateAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual List<Item> Items { get; set; }
        
        
    }
    
}