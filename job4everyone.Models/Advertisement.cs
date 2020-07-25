﻿using System;
using System.Collections.Generic;
using System.Text;

namespace job4everyone.Models
{
    public class Advertisement
    {
        public Advertisement()
        {
            this.CreatedAt = DateTime.UtcNow;
            this.UpdatedAt = DateTime.UtcNow;
            this.Active = false;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int JobPositionId { get; set; }
        public JobPosition JobPosition { get; set; }
        public int EmployerId { get; set; }
        public Employer Employer { get; set; }
        public List<AdvertisementCandidate> Candidates { get; set; }
    }
}
