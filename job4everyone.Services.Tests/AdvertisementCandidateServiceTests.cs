using job4everyone.Data;
using job4everyone.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Frameworks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace job4everyone.Services.Tests
{
    class AdvertisementCandidateServiceTests
    {
        private DbContextOptions<Job4EveryoneDbContext> contextOptions;
        private Job4EveryoneDbContext context;
        private Candidate candidate;
        private Advertisement advertisement;

        public AdvertisementCandidateServiceTests()
        {
            this.contextOptions = new DbContextOptionsBuilder<Job4EveryoneDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [SetUp]
        public void SetUp()
        {
            this.context = new Job4EveryoneDbContext(this.contextOptions);
            this.context.SaveChanges();
            var candidate = new Candidate();
            var advertisement = new Advertisement();
            this.context.Candidates.Add(candidate);
            this.context.Advertisements.Add(advertisement);
            this.context.SaveChanges();

            this.advertisement = advertisement;
            this.candidate = candidate;
        }

        [TearDown]
        public void CleanUpDatabase()
        {
            this.context.Database.EnsureDeleted();
            this.context = null;
        }
        [Test]
        public void Candidate_CanBeAddedToAdvertisement()
        {
            var service = new AdvertisementCandidateService(context);

            service.AddCandidateToAdvertisement(this.advertisement.Id, this.candidate.Id);

            Assert.AreEqual(1, this.context.AdvertisementsCandidates.Count());
        }
        [Test]
        public void CandidateWithInvalidId_WhenAddedToAdvertisement_ThrowsException()
        {
            var service = new AdvertisementCandidateService(context);

            var ex = Assert.Throws<ArgumentException>(() => service.AddCandidateToAdvertisement(this.advertisement.Id, 2));
            Assert.That(ex.Message, Is.EqualTo("Invalid candidate id. (Parameter 'candidateId')"));
        }
        [Test]
        public void Candidate_WhenAddedToAdvertisementWithInvalidId_ThrowsException()
        {
            var service = new AdvertisementCandidateService(context);

            var ex = Assert.Throws<ArgumentException>(() => service.AddCandidateToAdvertisement(2, this.candidate.Id));
            Assert.That(ex.Message, Is.EqualTo("Invalid advertisement id. (Parameter 'advertisementId')"));
        }
        [Test]
        public void Candidate_CanBeRemovedFromAdvertisement()
        {
            var otherCandidate = new Candidate();
            var relation1 = new AdvertisementCandidate() {Advertisement = this.advertisement, Candidate = this.candidate};
            var relation2 = new AdvertisementCandidate() {Advertisement = this.advertisement, Candidate = otherCandidate};
            advertisement.Candidates.Add(relation1);
            advertisement.Candidates.Add(relation2);
            candidate.Advertisements.Add(relation1);
            otherCandidate.Advertisements.Add(relation2);
            this.context.SaveChanges();

            var service = new AdvertisementCandidateService(context);

            Assert.AreEqual(2, this.context.AdvertisementsCandidates.Count());
            service.RemoveCandidateFromAdvertisement(this.advertisement.Id, this.candidate.Id);
            Assert.AreEqual(1, this.context.AdvertisementsCandidates.Count());
        }
        [Test]
        public void CandidateWithInvalidId_WhenRemovedFromAdvertisement_ThrowsException()
        {
            var service = new AdvertisementCandidateService(context);

            var ex = Assert.Throws<ArgumentException>(() => service.RemoveCandidateFromAdvertisement(this.advertisement.Id, 2));
            Assert.That(ex.Message, Is.EqualTo("Invalid candidate id. (Parameter 'candidateId')"));
        }
        [Test]
        public void Candidate_WhenRemovedFromAdvertisementWithInvalidId_ThrowsException()
        {
            var service = new AdvertisementCandidateService(context);

            var ex = Assert.Throws<ArgumentException>(() => service.RemoveCandidateFromAdvertisement(2, this.candidate.Id));
            Assert.That(ex.Message, Is.EqualTo("Invalid advertisement id. (Parameter 'advertisementId')"));
        }
        [Test]
        public void CandidateWithoutRelationToAdvertisement_WhenRemoved_ThrowsException()
        {
            var service = new AdvertisementCandidateService(context);

            var ex = Assert.Throws<ArgumentException>(() => service.RemoveCandidateFromAdvertisement(this.advertisement.Id, this.candidate.Id));
            Assert.That(ex.Message, Is.EqualTo("Candidate does not have relation to advertisement (Parameter 'candidateId')"));
        }
    }
}