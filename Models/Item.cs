using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class Item
    {
        public Guid Id { get; set; }

        [Required]
        public String Description { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public DateTime CreateAt { get; set; }
    }
}