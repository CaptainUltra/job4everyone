using job4everyone.Data;
using job4everyone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace job4everyone.Services
{
    public interface IAdvertisementCandidateService
    {
        void AddCandidateToAdvertisement(int advertisementId, int candidateId);
        void RemoveCandidateFromAdvertisement(int advertisementId, int candidateId);
    }
    public class AdvertisementCandidateService : IAdvertisementCandidateService
    {
        private Job4EveryoneDbContext context;

        public AdvertisementCandidateService(Job4EveryoneDbContext context)
        {
            this.context = context;
        }
        public void AddCandidateToAdvertisement(int advertisementId, int candidateId)
        {
            var advertisement = this.context.Advertisements.FirstOrDefault(a => a.Id == advertisementId);
            if(advertisement == null)
            {
                throw new ArgumentException("Invalid advertisement id.", "advertisementId");
            }

            var candidate = this.context.Candidates.FirstOrDefault(c => c.Id == candidateId);
            if(candidate == null)
            {
                throw new ArgumentException("Invalid candidate id.", "candidateId");
            }
            var relation = new AdvertisementCandidate() {Advertisement = advertisement, Candidate = candidate};
            
            advertisement.Candidates.Add(relation);
            candidate.Advertisements.Add(relation);

            this.context.SaveChanges();
        }

        public void RemoveCandidateFromAdvertisement(int advertisementId, int candidateId)
        {
            var advertisement = this.context.Advertisements.FirstOrDefault(a => a.Id == advertisementId);
            if(advertisement == null)
            {
                throw new ArgumentException("Invalid advertisement id.", "advertisementId");
            }

            var candidate = this.context.Candidates.FirstOrDefault(c => c.Id == candidateId);
            if(candidate == null)
            {
                throw new ArgumentException("Invalid candidate id.", "candidateId");
            }

            var relation = this.context.AdvertisementsCandidates.FirstOrDefault(r => r.AdvertisementId == advertisement.Id && r.CandidateId == candidate.Id);
            if(relation == null)
            {
                throw new ArgumentException("Candidate does not have relation to advertisement", "candidateId");
            }
            advertisement.Candidates.Remove(relation);
            candidate.Advertisements.Remove(relation);

            this.context.SaveChanges();
            
        }
    }
}