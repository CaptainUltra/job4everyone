using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace job4everyone.Models
{
    public class Employer
    {
        public Employer()
        {
            this.CreatedAt = DateTime.UtcNow;
            this.UpdatedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "varchar(32)")]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Advertisement> Advertisements { get; set; }
    }
}
