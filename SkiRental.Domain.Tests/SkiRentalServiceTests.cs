using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkiRental.Domain.Entities;
using SkiRental.Domain.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SkiRental.Domain.Tests
{
    [TestClass]
    public class SkiRentalServiceTests
    {
        private RepositoryStub repository;

        private SkiRentalService service;

        [TestInitialize]
        public void TestInitialize()
        {
            repository = new RepositoryStub();

            service = new SkiRentalService(repository);
        }

        [TestMethod]
        public async Task RegisterSkiAsync_CreatesNewSkiEntry()
        {
            // Arrange
            string name = "test";
            double rate = 10;

            // Act
            await service.RegisterSkiAsync(name, rate);

            // Assert;
            var ski = this.repository.Items.FirstOrDefault();
            Assert.IsNotNull(ski);
            Assert.AreEqual(name, ski.Name);
            Assert.AreEqual(rate, ski.Rate);
            Assert.IsNull(ski.RentTime);
            Assert.IsNull(ski.CustomerName);
        }

        [TestMethod]
        public async Task RentSkiAsync_ThrowsIfSkiIsNotFound()
        {
            // Act && Assert
            await Assert.ThrowsExceptionAsync<SkiException>(() => this.service.RentSkiAsync(234, "test"));
        }

        [TestMethod]
        public async Task RentSkiAsync_ThrowsIfSkiAlreadyRented()
        {
            // Arrange
            var ski = new Ski
            {
                Id = 1,
                Name = "test ski",
                Rate = 5,
                RentTime = DateTime.Today,
                CustomerName = "customer"
            };
            this.repository.Items.Add(ski);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<SkiException>(() => this.service.RentSkiAsync(1, "customer 2"));
        }

        [TestMethod]
        public async Task RentSkiAsync_RenstAvailableSki()
        {
            var ski = new Ski
            {
                Id = 1,
                Name = "test ski",
                Rate = 5
            };
            this.repository.Items.Add(ski);

            // Act
            await this.service.RentSkiAsync(1, "customer 1");

            // Assert
            var storedSki = this.repository.Items.FirstOrDefault(x => x.Id == ski.Id);
            Assert.IsNotNull(storedSki);
            Assert.IsNotNull(storedSki.RentTime);
            Assert.AreEqual("customer 1", storedSki.CustomerName);
        }

        [TestMethod]
        public async Task ReturnRentedSkiAsync_ThrowsIfSkiIsNotFound()
        {
            // Act && Assert
            await Assert.ThrowsExceptionAsync<SkiException>(() => this.service.ReturnRentedSkiAsync(234));
        }

        [TestMethod]
        public async Task ReturnRentedSkiAsync_ThrowsIfSkiIsNotRented()
        {
            // Arrange
            var ski = new Ski
            {
                Id = 1,
                Name = "test ski",
                Rate = 5
            };
            this.repository.Items.Add(ski);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<SkiException>(() => this.service.ReturnRentedSkiAsync(ski.Id));
        }

        [TestMethod]
        public async Task ReturnRentedSkiAsync_ReturnsRentedSki()
        {
            // Arrange
            var ski = new Ski
            {
                Id = 1,
                Name = "test ski",
                Rate = 5,
                RentTime = DateTime.Now,
                CustomerName = "customer 1",
            };
            this.repository.Items.Add(ski);

            // Act
            var rentCost = await this.service.ReturnRentedSkiAsync(ski.Id);
            var storedSki = this.repository.Items.FirstOrDefault(x => x.Id == ski.Id);
            Assert.IsNotNull(storedSki);
            Assert.IsNull(storedSki.RentTime);
            Assert.IsNull(storedSki.CustomerName);
            Assert.AreEqual("customer 1", rentCost.CustomerName);
            Assert.AreEqual(ski.Id, rentCost.SkiId);
        }
    }
}
