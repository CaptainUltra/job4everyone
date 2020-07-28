using job4everyone.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace job4everyone.Data
{
    public class Job4EveryoneDbContext : IdentityDbContext<Employer>
    {

        public Job4EveryoneDbContext(DbContextOptions<Job4EveryoneDbContext> options)
            : base(options)
        { }

        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<AdvertisementCandidate> AdvertisementsCandidates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(Configuration.ConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region JobPosition seeding
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
            #endregion

            modelBuilder.Entity<AdvertisementCandidate>()
            .HasKey(x => new { x.AdvertisementId, x.CandidateId });

            modelBuilder.Entity<AdvertisementCandidate>()
                .HasOne(ac => ac.Advertisement)
                .WithMany(a => a.Candidates)
                .HasForeignKey(ac => ac.AdvertisementId);

            modelBuilder.Entity<AdvertisementCandidate>()
                .HasOne(ac => ac.Candidate)
                .WithMany(c => c.Advertisements)
                .HasForeignKey(ac => ac.CandidateId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
