﻿using job4everyone.Data;
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
    class AdvertisementServiceTests
    {
        private DbContextOptions<Job4EveryoneDbContext> contextOptions;
        private Job4EveryoneDbContext context;
        private int jobPositionId;
        private string employerUserName;
        private string employerId;

        public AdvertisementServiceTests()
        {
            this.contextOptions = new DbContextOptionsBuilder<Job4EveryoneDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [SetUp]
        public void SetUp()
        {
            this.context = new Job4EveryoneDbContext(this.contextOptions);
            var jobPosition = new JobPosition() { Name = "Job position" };
            var employer = new Employer() { UserName = "Employer1" };
            this.context.JobPositions.Add(jobPosition);
            this.context.Employers.Add(employer);
            this.context.SaveChanges();
            this.jobPositionId = jobPosition.Id;
            this.employerUserName = employer.UserName;
            this.employerId = employer.Id;
        }

        [TearDown]
        public void CleanUpDatabase()
        {
            this.context.Database.EnsureDeleted();
            this.context = null;
        }

        [Test]
        public void Advertisement_CanBeCreated()
        {
            var service = new AdvertisementService(context);

            var advertisement = service.CreateAdvertisement("Advertisement1", "An advertisement for a job.", true, this.jobPositionId, this.employerUserName);

            Assert.AreEqual("Advertisement1", advertisement.Name);
            Assert.AreEqual(1, advertisement.Id);
            Assert.AreEqual(1, context.Advertisements.Count());
            Assert.AreEqual(jobPositionId, advertisement.JobPositionId);
            Assert.AreEqual(this.employerId, advertisement.EmployerId);
            Assert.IsNotNull(advertisement.JobPosition);
            Assert.IsNotNull(advertisement.Employer);
        }
        [Test]
        public void AdvertisementWithInvalidJobPositionId_ThrowsException_WhenCreated()
        {
            var service = new AdvertisementService(context);

            var ex = Assert.Throws<ArgumentException>(() => service.CreateAdvertisement("Advertisement1", "An advertisement for a job.", true, 2, this.employerUserName));
            Assert.That(ex.Message, Is.EqualTo("Invalid job position id. (Parameter 'id')"));
        }
        [Test]
        public void AdvertisementWithInvalidEmployer_ThrowsException_WhenCreated()
        {
            var service = new AdvertisementService(context);

            var ex = Assert.Throws<ArgumentException>(() => service.CreateAdvertisement("Advertisement1", "An advertisement for a job.", true, this.jobPositionId, "Employer2"));
            Assert.That(ex.Message, Is.EqualTo("Invalid employer user name. (Parameter 'employerUserName')"));
        }
        [Test]
        public void Advertisement_ActiveIsSetToFalse_WhenThereAre10AlreadyActiveForThatEmployer_Create()
        {
            var service = new AdvertisementService(context);
            for (int i = 1; i <= 10; i++)
            {
                context.Advertisements.Add(new Advertisement { Name = "Advertisement1", Description = "An advertisement for a job.", Active = true, JobPositionId = this.jobPositionId, EmployerId = this.employerId });
            }
            this.context.SaveChanges();

            var advertisement = service.CreateAdvertisement("Advertisement1", "An advertisement for a job.", true, this.jobPositionId, this.employerUserName);
            Assert.IsFalse(advertisement.Active);
        }
        [Test]
        public void AdvertisementOfASingleEmployer_TestSetAllToInactive()
        {
            var service = new AdvertisementService(context);
            for (int i = 1; i <= 5; i++)
            {
                context.Advertisements.Add(new Advertisement { Name = "Advertisement1", Description = "An advertisement for a job.", Active = true, JobPositionId = this.jobPositionId, EmployerId = this.employerId });
            }
            var employer = new Employer();
            context.Employers.Add(employer);
            var employerId = employer.Id;
            context.Advertisements.Add(new Advertisement { Name = "Advertisement2", Description = "An advertisement for a job.", Active = true, JobPositionId = this.jobPositionId, EmployerId = employerId });
            this.context.SaveChanges();

            service.ChangeAllAdvertisementsToInactive(this.employerUserName);

            Assert.AreEqual(1, this.context.Advertisements.Count(a => a.Active == true));
        }
        [Test]
        public void Advertisement_ReturnAllAdvertisementsByEmployer()
        {
            var service = new AdvertisementService(context);
            context.Advertisements.Add(new Advertisement { Name = "Advertisement1", Description = "An advertisement for a job.", Active = true, JobPositionId = this.jobPositionId, EmployerId = this.employerId });
            var employer = new Employer();
            context.Employers.Add(employer);
            var employerId = employer.Id;
            context.Advertisements.Add(new Advertisement { Name = "Advertisement2", Description = "An advertisement for a job.", Active = true, JobPositionId = this.jobPositionId, EmployerId = employerId });
            this.context.SaveChanges();

            var advertisements = service.GetAdvertisementsListByEmployer(this.employerUserName);

            Assert.AreEqual(1, advertisements.Count());
        }
        [Test]
        public void Advertisement_ReturnAllAdvertisementsByEmployer_WithInvalidEmployerName_ThrowsException()
        {
            var service = new AdvertisementService(context);

            var ex = Assert.Throws<ArgumentException>(() => service.GetAdvertisementsListByEmployer(""));
            Assert.That(ex.Message, Is.EqualTo("Invalid employer user name. (Parameter 'employerUserName')"));
        }
        [Test]
        public void AdvertisementOfASingleEmployer_SetAllToInactive_WithInvalidUserName_ThrowsException()
        {
            var service = new AdvertisementService(context);

            var ex = Assert.Throws<ArgumentException>(() => service.ChangeAllAdvertisementsToInactive(""));
            Assert.That(ex.Message, Is.EqualTo("Invalid employer user name. (Parameter 'employerUserName')"));
        }
        [Test]
        public void AdvertisementOfASingleEmployer_TestSetLast10ToActive()
        {
            var service = new AdvertisementService(context);
            for (int i = 1; i <= 10; i++)
            {
                context.Advertisements.Add(new Advertisement { Name = "Advertisement1", Description = "An advertisement for a job.", Active = false, JobPositionId = this.jobPositionId, EmployerId = this.employerId });
            }
            var employer = new Employer();
            context.Employers.Add(employer);
            var employerId = employer.Id;
            context.Advertisements.Add(new Advertisement { Name = "Advertisement2", Description = "An advertisement for a job.", Active = false, JobPositionId = this.jobPositionId, EmployerId = employerId });
            this.context.SaveChanges();

            service.ChangeLast10AdvertisementsToActive(this.employerUserName);

            Assert.AreEqual(1, this.context.Advertisements.Count(a => a.Active == false));
        }
        [Test]
        public void AdvertisementOfASingleEmployer_SetLast10ToActive_WithInvalidUserName_ThrowsException()
        {
            var service = new AdvertisementService(context);

            var ex = Assert.Throws<ArgumentException>(() => service.ChangeLast10AdvertisementsToActive(""));
            Assert.That(ex.Message, Is.EqualTo("Invalid employer user name. (Parameter 'employerUserName')"));
        }
        [Test]
        public void Advertisement_CanBeRetrieved()
        {
            context.Advertisements.Add(new Advertisement { Name = "Advertisement1", Description = "An advertisement for a job.", Active = true, JobPositionId = this.jobPositionId });
            context.SaveChanges();
            var service = new AdvertisementService(context);

            var advertisement = service.GetAdvertisement(1);

            Assert.AreEqual(1, advertisement.Id);
            Assert.AreEqual("Advertisement1", advertisement.Name);
        }
        [Test]
        public void AdvertisementWithInvalidId_WhenRetrieved_ThrowsExeption()
        {
            context.Advertisements.Add(new Advertisement { Name = "Advertisement" });
            context.SaveChanges();
            var service = new AdvertisementService(context);

            var ex = Assert.Throws<ArgumentException>(() => service.GetAdvertisement(2));
            Assert.That(ex.Message, Is.EqualTo("Invalid advertisement id. (Parameter 'id')"));
        }
        [Test]
        public void AdvertisementsList_CanBeRetrieved()
        {
            context.Advertisements.Add(new Advertisement { Name = "Advertisement1", Description = "An advertisement for a job.", Active = true, JobPositionId = this.jobPositionId });
            context.Advertisements.Add(new Advertisement { Name = "Advertisement2", Description = "An advertisement for a job.", Active = true, JobPositionId = this.jobPositionId });
            context.SaveChanges();
            var service = new AdvertisementService(context);

            var advertisements = service.GetAdvertisementsList();

            Assert.AreEqual(2, advertisements.Count);
        }
        [Test]
        public void Advertisement_CanBeUpdated()
        {
            var jobPosition = new JobPosition() { Name = "Jpos2" };
            context.JobPositions.Add(jobPosition);
            context.Advertisements.Add(new Advertisement { Name = "Advertisement1", Description = "An advertisement for a job.", Active = true, JobPositionId = this.jobPositionId });
            context.SaveChanges();
            var service = new AdvertisementService(context);
            var updateData = new Advertisement { Name = "Advertisement2" , Active = false, JobPosition = jobPosition };

            var advertisement = service.UpdateAdvertisement(1, updateData);
            var advertisementRecord = context.Advertisements.Single(a => a.Name == "Advertisement2");

            Assert.AreEqual("Advertisement2", advertisement.Name);
            Assert.AreEqual(false, advertisement.Active);
            Assert.AreEqual("Advertisement2", advertisementRecord.Name);
            Assert.AreEqual(2, advertisement.JobPositionId);
        }
        [Test]
        public void AdvertisementWithInvalidId_WhenUpdated_ThrowsExeption()
        {
            var service = new AdvertisementService(context);
            var updateData = new Advertisement { Name = "Advertisement1", Description = "An advertisement for a job.", Active = true, JobPositionId = this.jobPositionId };

            var ex = Assert.Throws<ArgumentException>(() => service.UpdateAdvertisement(1, updateData));
            Assert.That(ex.Message, Is.EqualTo("Invalid advertisement id. (Parameter 'id')"));
        }
        [Test]
        public void Advertisement_CanBeDeleted()
        {
            context.Advertisements.Add(new Advertisement { Name = "Advertisement1", Description = "An advertisement for a job.", Active = true, JobPositionId = this.jobPositionId });
            context.SaveChanges();
            var service = new AdvertisementService(context);

            service.DeleteAdvertisement(1);

            Assert.AreEqual(0, context.Advertisements.Count());
        }
        [Test]
        public void AdvertisementWithInvalidId_WhenDeleted_ThrowsExeption()
        {
            var service = new AdvertisementService(context);

            var ex = Assert.Throws<ArgumentException>(() => service.DeleteAdvertisement(1));
            Assert.That(ex.Message, Is.EqualTo("Invalid advertisement id. (Parameter 'id')"));
        }
    }
}
