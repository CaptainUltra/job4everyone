﻿using job4everyone.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace job4everyone.Data
{
    public class Job4EveryoneDbContext : DbContext
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
