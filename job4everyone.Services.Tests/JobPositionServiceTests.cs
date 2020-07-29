using job4everyone.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using job4everyone.Services;
using System.Linq;
using job4everyone.Models;

namespace job4everyone.Services.Tests
{
    public class JobPositionServiceTests
    {
        private DbContextOptions<Job4EveryoneDbContext> contextOptions;
        private Job4EveryoneDbContext context;

        public JobPositionServiceTests()
        {
            this.contextOptions = new DbContextOptionsBuilder<Job4EveryoneDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [SetUp]
        public void SetUp()
        {
            this.context = new Job4EveryoneDbContext(this.contextOptions);
        }

        [TearDown]
        public void CleanUpDatabase()
        {
            this.context.Database.EnsureDeleted();
            this.context = null;
        }

        [Test]
        public void JobPosition_CanBeCreated()
        {
            var service = new JobPositionService(context);

            var jobPosition = service.CreateJobPosition("Developer");

            Assert.AreEqual("Developer", jobPosition.Name);
            Assert.AreEqual(1, jobPosition.Id);
            Assert.AreEqual(1, context.JobPositions.Count());
        }
        [Test]
        public void JobPosition_WithMoreThan30CharectersName_WhenCreated_ThrowsException()
        {
            var service = new JobPositionService(context);

            var jobPosition = service.CreateJobPosition("JobPositionWith31CharectersName");

            Assert.AreEqual("JobPositionWith31CharectersName", jobPosition.Name);
            Assert.AreEqual(1, jobPosition.Id);
            Assert.AreEqual(1, context.JobPositions.Count());
        }
        [Test]
        public void JobPosition_CanBeRetrieved()
        {
            context.JobPositions.Add(new JobPosition { Name = "Developer" });
            context.SaveChanges();
            var service = new JobPositionService(context);

            var jobPosition = service.GetJobPosition(1);

            Assert.AreEqual(1, jobPosition.Id);
            Assert.AreEqual("Developer", jobPosition.Name);
        }
        [Test]
        public void JobPositionWithInvalidId_WhenRetrieved_ThrowsExeption()
        {
            context.JobPositions.Add(new JobPosition { Name = "JobPosition" });
            context.SaveChanges();
            var service = new JobPositionService(context);

            var ex = Assert.Throws<ArgumentException>(() => service.GetJobPosition(2));
            Assert.That(ex.Message, Is.EqualTo("Invalid job position id. (Parameter 'id')"));
        }
        [Test]
        public void JobPositionsList_CanBeRetrieved()
        {
            context.JobPositions.Add(new JobPosition { Name = "JobPosition" });
            context.JobPositions.Add(new JobPosition { Name = "JobPosition2" });
            context.SaveChanges();
            var service = new JobPositionService(context);

            var jobPositions = service.GetJobPositionsList();

            Assert.AreEqual(2, jobPositions.Count);
        }
        [Test]
        public void JobPosition_CanBeUpdated()
        {
            context.JobPositions.Add(new JobPosition { Name = "JobPosition" });
            context.SaveChanges();
            var service = new JobPositionService(context);
            var updateData = new JobPosition { Name = "JobPosition2" };

            var jobPosition = service.UpdateJobPosition(1, updateData);
            var jobPositionRecord = context.JobPositions.Single(i => i.Name == "JobPosition2");

            Assert.AreEqual("JobPosition2", jobPosition.Name);
            Assert.AreEqual("JobPosition2", jobPositionRecord.Name);
        }
        [Test]
        public void JobPositionWithInvalidId_WhenUpdated_ThrowsExeption()
        {
            var service = new JobPositionService(context);
            var updateData = new JobPosition { Name = "JobPosition" };

            var ex = Assert.Throws<ArgumentException>(() => service.UpdateJobPosition(1, updateData));
            Assert.That(ex.Message, Is.EqualTo("Invalid job position id. (Parameter 'id')"));
        }
        [Test]
        public void JobPosition_CanBeDeleted()
        {
            context.JobPositions.Add(new JobPosition { Name = "JobPosition" });
            context.SaveChanges();
            var service = new JobPositionService(context);

            service.DeleteJobPosition(1);

            Assert.AreEqual(0, context.JobPositions.Count());
        }
        [Test]
        public void JobPositionWithInvalidId_WhenDeleted_ThrowsExeption()
        {
            var service = new JobPositionService(context);

            var ex = Assert.Throws<ArgumentException>(() => service.DeleteJobPosition(1));
            Assert.That(ex.Message, Is.EqualTo("Invalid job position id. (Parameter 'id')"));
        }
    }
}