using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using job4everyone.Data;
using job4everyone.Models;

namespace job4everyone.Services
{
    public interface ICandidateService
    {
        List<Candidate> GetCandidatesList();
        Candidate GetCandidate(int id);
        Candidate CreateCandidate(string firstName, string lastName, string email);
        Candidate UpdateCandidate(int id, Candidate candidate);
        void DeleteCandidate(int id);
    }
    public class CandidateService : ICandidateService
    {
        private Job4EveryoneDbContext context;

        public CandidateService(Job4EveryoneDbContext context)
        {
            this.context = context;
        }
        public Candidate CreateCandidate(string firstName, string lastName, string email)
        {
            var candidate = new Candidate()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
            };

            this.context.Candidates.Add(candidate);
            this.context.SaveChanges();

            return candidate;
        }

        public void DeleteCandidate(int id)
        {
            var candidate = this.context.Candidates.FirstOrDefault(i => i.Id == id);
            if (candidate == null)
            {
                throw new ArgumentException("Invalid candidate id.", "id");
            }
            this.context.Candidates.Remove(candidate);
            this.context.SaveChanges();
        }

        public Candidate GetCandidate(int id)
        {
            var candidate = this.context.Candidates.FirstOrDefault(i => i.Id == id);
            if (candidate == null)
            {
                throw new ArgumentException("Invalid candidate id.", "id");
            }
            return candidate;
        }

        public List<Candidate> GetCandidatesList()
        {
            var list = this.context.Candidates.ToList();

            return list;
        }

        public Candidate UpdateCandidate(int id, Candidate newCandidate)
        {
            var candidate = this.context.Candidates.FirstOrDefault(i => i.Id == id);
            if (candidate == null)
            {
                throw new ArgumentException("Invalid candidate id.", "id");
            }

            candidate.FirstName = newCandidate.FirstName;
            candidate.LastName = newCandidate.LastName;
            candidate.Email = newCandidate.Email;
            candidate.UpdatedAt = DateTime.UtcNow;

            this.context.SaveChanges();

            return candidate;
        }
    }
}
