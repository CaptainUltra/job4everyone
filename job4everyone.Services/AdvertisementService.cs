using job4everyone.Data;
using job4everyone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace job4everyone.Services
{
    public interface IAdvertisementService
    {
        List<Advertisement> GetAdvertisementsList();
        Advertisement GetAdvertisement(int id);
        Advertisement CreateAdvertisement(string name, string description, bool active, int jobPositionId, string employerUserName);
        Advertisement UpdateAdvertisement(int id, Advertisement advertisement);
        void DeleteAdvertisement(int id);
        void ChangeAllAdvertisementsToInactive();
        void ChangeLast10AdvertisementsToActive();
    }

    public class AdvertisementService : IAdvertisementService
    {
        private Job4EveryoneDbContext context;

        public AdvertisementService(Job4EveryoneDbContext context)
        {
            this.context = context;
        }
        public void ChangeAllAdvertisementsToInactive()
        {
            throw new NotImplementedException();
        }

        public void ChangeLast10AdvertisementsToActive()
        {
            throw new NotImplementedException();
        }

        public Advertisement CreateAdvertisement(string name, string description, bool active, int jobPositionId, string employerUserName)
        {
            var jobPosition = this.context.JobPositions.FirstOrDefault(jp => jp.Id == jobPositionId);
            if (jobPosition == null)
            {
                throw new ArgumentException("Invalid job position id.", "id");
            }

            var employer = this.context.Employers.FirstOrDefault(e => e.UserName == employerUserName);
            if(employer == null)
            {
                throw new ArgumentException("Invalid employer user name.", "employerUserName");
            }

            //TODO: Add check for 10 active

            var advertisement = new Advertisement() { Name = name, Description = description, Active = active};
            advertisement.JobPosition = jobPosition;
            advertisement.Employer = employer;
            this.context.Add(advertisement);
            this.context.SaveChanges();

            return advertisement;
        }

        public void DeleteAdvertisement(int id)
        {
            var advertisement = this.context.Advertisements.FirstOrDefault(a => a.Id == id);
            if (advertisement == null)
            {
                throw new ArgumentException("Invalid advertisement id.", "id");
            }
            this.context.Advertisements.Remove(advertisement);
            this.context.SaveChanges();
        }

        public Advertisement GetAdvertisement(int id)
        {
            var advertisement = this.context.Advertisements.FirstOrDefault(a => a.Id == id);
            if (advertisement == null)
            {
                throw new ArgumentException("Invalid advertisement id.", "id");
            }

            return advertisement;
        }

        public List<Advertisement> GetAdvertisementsList()
        {
            var list = this.context.Advertisements.ToList();
            
            return list;
        }

        public Advertisement UpdateAdvertisement(int id, Advertisement newAdvertisement)
        {
            var advertisement = this.context.Advertisements.FirstOrDefault(a => a.Id == id);
            if (advertisement == null)
            {
                throw new ArgumentException("Invalid advertisement id.", "id");
            }

            advertisement.Name = newAdvertisement.Name;
            advertisement.Description = newAdvertisement.Description;
            advertisement.Active = newAdvertisement.Active;
            advertisement.JobPosition = newAdvertisement.JobPosition;
            advertisement.UpdatedAt = DateTime.UtcNow;

            this.context.SaveChanges();

            return advertisement;
        }
    }
}
