using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckLibrary.Domain.Models;

namespace TruckLibrary.Domain.Repositories
{
    /// <summary>
    /// Repository interface for <see cref="Truck"/> entity.
    /// </summary>
    public interface ITruckRepository
    {
        public Task AddAsync(Truck truck);
     
        public Task<Truck?> GetByIdAsync(Guid id);

        public Task<List<Truck>> GetAllAsync();
        public Task Remove(Truck truck);

        public Task<Truck?> Update(Truck truck);
    }
}