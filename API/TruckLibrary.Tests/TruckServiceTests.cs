using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruckLibrary.Core.Services;
using TruckLibrary.Domain.Models;
using TruckLibrary.Domain.Repositories;
using Xunit;

namespace TruckLibrary.Tests
{
    public class TruckServiceTests
    {
        private readonly Mock<ITruckRepository> _repository;
        private readonly TruckService _service;

        public TruckServiceTests()
        {
            _repository = new Mock<ITruckRepository>();
            _service = new TruckService(_repository.Object);
        }

        [Fact]
        public async Task AddAsync_ValidTruck_CallsRepository()
        {
            // Arrange
            var truck = new Truck
            {
                Model = "FH",
                ManufacturingYear = DateTime.UtcNow.Year,
                ChassisCode = "ABC123DEF",
                Color = "Red",
                ManufacturingPlant = "Brazil"
            };

            // Act
            await _service.AddAsync(truck);

            // Assert
            _repository.Verify(r => r.AddAsync(truck), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("INVALID")]
        public async Task AddAsync_InvalidModel_ThrowsArgumentException(string model)
        {
            var truck = new Truck
            {
                Model = model,
                ManufacturingYear = DateTime.UtcNow.Year,
                ChassisCode = "ABC123DEF",
                Color = "Red",
                ManufacturingPlant = "Brazil"
            };

            await Assert.ThrowsAsync<ArgumentException>(() => _service.AddAsync(truck));
        }

        [Theory]
        [InlineData(1927)]
        [InlineData(1800)]
        [InlineData(9999)]
        public async Task AddAsync_InvalidManufacturingYear_ThrowsArgumentException(int year)
        {
            var truck = new Truck
            {
                Model = "FH",
                ManufacturingYear = year,
                ChassisCode = "ABC123DEF",
                Color = "Red",
                ManufacturingPlant = "Brazil"
            };

            await Assert.ThrowsAsync<ArgumentException>(() => _service.AddAsync(truck));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("12345678")] // 8 chars
        [InlineData("1234567890")] // 10 chars
        [InlineData("12345678!")] // 9 chars, invalid char
        public async Task AddAsync_InvalidChassisCode_ThrowsArgumentException(string chassisCode)
        {
            var truck = new Truck
            {
                Model = "FH",
                ManufacturingYear = DateTime.UtcNow.Year,
                ChassisCode = chassisCode,
                Color = "Red",
                ManufacturingPlant = "Brazil"
            };

            await Assert.ThrowsAsync<ArgumentException>(() => _service.AddAsync(truck));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("Germany")]
        public async Task AddAsync_InvalidManufacturingPlant_ThrowsArgumentException(string plant)
        {
            var truck = new Truck
            {
                Model = "FH",
                ManufacturingYear = DateTime.UtcNow.Year,
                ChassisCode = "ABC123DEF",
                Color = "Red",
                ManufacturingPlant = plant
            };

            await Assert.ThrowsAsync<ArgumentException>(() => _service.AddAsync(truck));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("ThisColorNameIsWayTooLongToBeValid")]
        public async Task AddAsync_InvalidColor_ThrowsArgumentException(string color)
        {
            var truck = new Truck
            {
                Model = "FH",
                ManufacturingYear = DateTime.UtcNow.Year,
                ChassisCode = "ABC123DEF",
                Color = color,
                ManufacturingPlant = "Brazil"
            };

            await Assert.ThrowsAsync<ArgumentException>(() => _service.AddAsync(truck));
        }

        /// From here below, tests for CRUD operations without business logic

        [Fact]
        public async Task GetAllAsync_ReturnsListFromRepository()
        {
            // Arrange
            var trucks = new List<Truck> { new Truck { Model = "FH" } };
            _repository.Setup(r => r.GetAllAsync()).ReturnsAsync(trucks);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(trucks, result);
            _repository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsTruckFromRepository()
        {
            // Arrange
            var id = Guid.NewGuid();
            var truck = new Truck { Id = id, Model = "FM" };
            _repository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(truck);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.Equal(truck, result);
            _repository.Verify(r => r.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task Remove_CallsRepository()
        {
            // Arrange
            var truck = new Truck { Model = "VM" };

            // Act
            await _service.Remove(truck);

            // Assert
            _repository.Verify(r => r.Remove(truck), Times.Once);
        }

        [Fact]
        public async Task Update_CallsRepositoryAndReturnsResult()
        {
            // Arrange
            var truck = new Truck { 
                Model = "FH" ,
                ManufacturingYear = 2025,
                ChassisCode = "123456789",
                Color = "Red",
                ManufacturingPlant = "Brazil"
            };
            
            _repository.Setup(r => r.Update(truck)).ReturnsAsync(truck);

            // Act
            var result = await _service.Update(truck);

            // Assert
            Assert.Equal(truck, result);
            _repository.Verify(r => r.Update(truck), Times.Once);
        }
    }
}