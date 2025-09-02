using Microsoft.EntityFrameworkCore;
using TruckLibrary.Core.DataContext;
using TruckLibrary.Domain.Models;
using TruckLibrary.Domain.Repositories;

namespace TruckLibrary.Core.Repositories
{
    public class TruckRepository: ITruckRepository
    {
        public TruckLibraryDbContext context { get; }
        public TruckRepository (TruckLibraryDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Truck truck)
        {
            await context.Trucks.AddAsync(truck);
            await context.SaveChangesAsync();
        }

        public Task<List<Truck>> GetAllAsync()
        {
            return context.Trucks.ToListAsync();
        }

        public Task<Truck?> GetByIdAsync(Guid id)
        {
            return context.Trucks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Remove(Truck truck)
        {
            context.Trucks.Remove(truck);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Update the database entity with the values from new entity.
        /// If entity doesnt exist in the database, returns null.
        /// </summary>
        /// <param name="truck">Entity with new values</param>
        /// <returns>Database entity updated.</returns>
        public async Task<Truck?> Update(Truck truck)
        {
            var truckDb = context.Trucks.FirstOrDefault(x => x.Id == truck.Id);
            if (truckDb == null)
                return null;

            truckDb.ManufacturingYear = truck.ManufacturingYear;
            truckDb.ManufacturingPlant = truck.ManufacturingPlant;
            truckDb.ChassisCode = truck.ChassisCode;
            truckDb.Color = truck.Color;
            truckDb.Model = truck.Model;

            await context.SaveChangesAsync();
            return truckDb;
        }
    }
}
