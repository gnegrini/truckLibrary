using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckLibrary.Core.DataContext;
using TruckLibrary.Domain.Models;
using TruckLibrary.Domain.Services;

namespace TruckLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrucksController : ControllerBase
    {
        private readonly ITruckService truckService;

        public TrucksController(ITruckService truckService)
        {
            this.truckService = truckService;
        }

        // GET: api/Trucks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Truck>>> GetTrucks()
        {
            var trucks = await truckService.GetAllAsync();
            return Ok(trucks);            
        }

        // GET: api/Trucks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Truck>> GetTruck(Guid id)
        {
            var truck = await truckService.GetByIdAsync(id);

            if (truck == null)
            {
                return NotFound();
            }

            return truck;
        }

        // PUT: api/Trucks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTruck(Guid id, Truck truck)
        {
            if (id != truck.Id)
            {
                return BadRequest();
            }

            var truckDb = await truckService.GetByIdAsync(id);
            if (truckDb == null)
            {
                return NotFound();
            }

            try
            {
                var newTruck = await truckService.Update(truck);
            } 
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

            return NoContent();
        }

        // POST: api/Trucks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Truck>> PostTruck(Truck truck)
        {
            try
            {
                await truckService.AddAsync(truck);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            return CreatedAtAction("GetTruck", new { id = truck.Id }, truck);
        }

        // DELETE: api/Trucks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTruck(Guid id)
        {
            var truck = await truckService.GetByIdAsync(id);
            if (truck == null)
            {
                return NotFound();
            }

            await truckService.Remove(truck);

            return NoContent();
        }
    }
}
