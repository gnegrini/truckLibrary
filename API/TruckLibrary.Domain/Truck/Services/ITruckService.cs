using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckLibrary.Domain.Models;

namespace TruckLibrary.Domain.Services
{
    /// <summary>
    /// Service interface for the <see cref="Truck"/> entity.
    /// </summary>
    public interface ITruckService
    {
        public Task<List<Truck>> GetAllAsync();

        public Task<Truck?> GetByIdAsync(Guid id);

        public Task AddAsync(Truck truck);

        public Task Remove(Truck truck);

        public Task<Truck?> Update(Truck truck);
    }
}
