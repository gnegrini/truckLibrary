using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckLibrary.Domain.Models;
using TruckLibrary.Domain.Repositories;
using TruckLibrary.Domain.Services;

namespace TruckLibrary.Core.Services
{
    public class TruckService: ITruckService
    {
        private readonly ITruckRepository truckRepository;
        private static readonly string[] ValidTruckModels = { "FH", "FM", "VM" };
        private static readonly string[] ValidManufacturingPlants = { "Brazil", "Sweden", "United States", "France" };

        public  TruckService(ITruckRepository truckRepository)
        {
            this.truckRepository = truckRepository;
        }

        public Task<List<Truck>> GetAllAsync()
        {
            return truckRepository.GetAllAsync();
        }

        public Task<Truck?> GetByIdAsync(Guid id)
        {
            return truckRepository.GetByIdAsync(id);
        }

        public Task AddAsync(Truck truck)
        {
            /// Do business validation before adding a new truck
            ValidateTruck(truck);
            return truckRepository.AddAsync(truck);
            
        }

        public Task Remove(Truck truck)
        {
            return truckRepository.Remove(truck);
        }

        /// <summary>
        /// Update the database entity with the values from new entity.
        /// If entity doesnt exist in the database, returns null.
        /// </summary>
        /// <param name="truck">Entity with new values</param>
        /// <returns>Database entity updated.</returns>
        public Task<Truck?> Update(Truck truck)
        {
            ValidateTruck(truck);
            return truckRepository.Update(truck);
        }

        /// <summary>
        /// Check the truck object against the business rules.
        /// </summary>
        /// <param name="truck"></param>
        /// <exception cref="ArgumentException">Exception in case one of the rules fails.</exception>
        private void ValidateTruck(Truck truck)
        {
            if (!IsValidModel(truck.Model))
                throw new ArgumentException("Invalid truck model. Allowed values are: FH, FM, VM.");

            if (!IsValidManufacturingYear(truck.ManufacturingYear))
                throw new ArgumentException($"Invalid manufacturing year. Allowed range is 1928 to {DateTime.UtcNow.Year + 1}.");

            if(!IsValidChassisCode(truck.ChassisCode))
                throw new ArgumentException("Invalid chassis code. It must be exactly 9 characters long and contain only letters and digits.");

            if(!IsValidManufacturingPlant(truck.ManufacturingPlant))
                throw new ArgumentException("Invalid manufacturing plant. Allowed values are: Brazil, Sweden, United States, France.");

            if(!IsValidColor(truck.Color))
                throw new ArgumentException("Invalid color. It must be non-empty and up to 20 characters long.");
        }

        /// <summary>
        /// Checks whether the model is one of the valid models: FH, FM or VM.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool IsValidModel(string model)
        {
            return ValidTruckModels.Contains(model, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Checks whether the manufacturing year is between 1928 and next year.
        /// - 1928 is the year of the first Volvo truck.
        /// - Next year is allowed because trucks can be manufacture as next year models.
        /// </summary>
        /// <param name="manufacturingYear"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private bool IsValidManufacturingYear(int manufacturingYear)
        {
            int currentYear = DateTime.UtcNow.Year;
            return !(manufacturingYear < 1928 || manufacturingYear > currentYear + 1);
        }
        /// <summary>
        /// Checks whether the chassis code is exactly 9 characters long and contains only letters and digits.
        /// </summary>
        /// <param name="chassisCode"></param>
        /// <returns></returns>
        private bool IsValidChassisCode(string chassisCode)
        {
            if (string.IsNullOrWhiteSpace(chassisCode) || chassisCode.Length != 9)
                return false;
            return chassisCode.All(char.IsLetterOrDigit);
        }

        private bool IsValidManufacturingPlant(string plant)
        {
            return ValidManufacturingPlants.Contains(plant, StringComparer.OrdinalIgnoreCase);
        }

        private bool IsValidColor(string color)
        {
            return !string.IsNullOrEmpty(color) && color.Length <= 20;
        }
    }
}
