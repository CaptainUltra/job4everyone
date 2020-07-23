using System;
using System.Collections.Generic;
using System.Text;

namespace job4everyone.Models
{
    public class Advertisements
    {
        public Advertisements()
        {
            this.CreatedAt = DateTime.UtcNow;
            this.UpdatedAt = DateTime.UtcNow;
            this.Active = false;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public JobPositions JobPositionId { get; set; }
        public Employers EmployerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
