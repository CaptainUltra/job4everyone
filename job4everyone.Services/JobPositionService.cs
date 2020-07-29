using job4everyone.Data;
using job4everyone.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace job4everyone.Services
{
    public interface IJobPositionService
    {
        List<JobPosition> GetJobPositionsList();
        JobPosition GetJobPosition(int id);
        JobPosition CreateJobPosition(string name);
        JobPosition UpdateJobPosition(int id, JobPosition jobPosition);
        void DeleteJobPosition(int id);
    }
    public class JobPositionService : IJobPositionService
    {
        private Job4EveryoneDbContext context;

        public JobPositionService(Job4EveryoneDbContext context)
        {
            this.context = context;
        }
        public JobPosition CreateJobPosition(string name)
        {
            var jobPosition = new JobPosition()
            {
                Name = name,
            };

            this.context.JobPositions.Add(jobPosition);
            this.context.SaveChanges();

            return jobPosition;
        }

        public void DeleteJobPosition(int id)
        {
            var jobPosition = this.context.JobPositions.FirstOrDefault(i => i.Id == id);
            if (jobPosition == null)
            {
                throw new ArgumentException("Invalid job position id.", "id");
            }
            this.context.JobPositions.Remove(jobPosition);
            this.context.SaveChanges();
        }

        public JobPosition GetJobPosition(int id)
        {
            var jobPosition = this.context.JobPositions.FirstOrDefault(i => i.Id == id);
            if (jobPosition == null)
            {
                throw new ArgumentException("Invalid job position id.", "id");
            }
            return jobPosition;
        }

        public List<JobPosition> GetJobPositionsList()
        {
            var list = this.context.JobPositions.ToList();

            return list;
        }

        public JobPosition UpdateJobPosition(int id, JobPosition newJobPosition)
        {
            var jobPosition = this.context.JobPositions.FirstOrDefault(i => i.Id == id);
            if (jobPosition == null)
            {
                throw new ArgumentException("Invalid job position id.", "id");
            }

            jobPosition.Name = newJobPosition.Name;
            jobPosition.UpdatedAt = DateTime.UtcNow;

            this.context.SaveChanges();

            return jobPosition;
        }
    }
}
