﻿using System;
using System.Collections.Generic;
using System.Text;

namespace job4everyone.Models
{
    public class JobPosition
    {
        public JobPosition()
        {
            this.CreatedAt = DateTime.UtcNow;
            this.UpdatedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Advertisement> Advertisements { get; set; }
    }
}