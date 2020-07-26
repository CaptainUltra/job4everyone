using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace job4everyone.Models
{
    public class Candidate
    {
        public Candidate()
        {
            this.CreatedAt = DateTime.UtcNow;
            this.UpdatedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<AdvertisementCandidate> Advertisements { get; set; }
    }
}
