using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Advertisement> Advertisements { get; set; }
        protected override void OnModelCreatingBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobPosition>().HasData(
                new JobPosition
                {
                    Id = 1,
                    Name = "QA"
                },
                new JobPosition
                {
                    Id = 2,
                    Name = "Developer"
                },
                new JobPosition
                {
                    Id = 3,
                    Name = "Manager"
                },
                new JobPosition
                {
                    Id = 4,
                    Name = "DevOps"
                },
                new JobPosition
                {
                    Id = 5,
                    Name = "PM"
                }
            );
        }
    }
}
