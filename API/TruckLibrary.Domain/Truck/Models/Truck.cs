using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckLibrary.Domain.Models
{
    /// <summary>
    /// Truck class
    /// </summary>
    public class Truck
    {
        public Truck()
        {

        }

        public Guid Id { get; set; }

        /// <summary>
        /// The model of the truck.
        /// Ex: FH, FM or VM.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// The year of manufacture of the truck.
        /// Ex: 2025.
        /// </summary>
        public int ManufacturingYear { get; set; }

        /// <summary>
        /// The chassis number of the truck.
        /// Ex: FD434FD43.
        /// </summary>
        public string ChassisCode { get; set; }

        /// <summary>
        /// Color of the truck.
        /// Ex: Red.
        /// </summary>
        public string Color {  get; set; }

        /// <summary>
        /// Plant of manufacture.
        /// Ex: Brazil, Sweden, United States, France.
        /// </summary>
        public string ManufacturingPlant { get; set; }



    }
}
