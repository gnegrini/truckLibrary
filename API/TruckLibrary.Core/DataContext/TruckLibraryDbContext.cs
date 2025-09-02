using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckLibrary.Domain.Models;

namespace TruckLibrary.Core.DataContext
{
    public class TruckLibraryDbContext : DbContext
    {
        public DbSet<Truck> Trucks { get; set; }

        public TruckLibraryDbContext(DbContextOptions<TruckLibraryDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Truck>(builder =>
            {
                builder.HasKey(p => p.Id);
                
                builder.Property(p => p.Model)
                       .IsRequired();

                builder.Property(p => p.ManufacturingYear)
                       .IsRequired();

                /// Using max length 9 as example
                builder.Property(p => p.ChassisCode)
                       .HasMaxLength(9)
                       .IsRequired();

                builder.Property(p => p.Color)
                       .HasMaxLength(20)
                       .IsRequired();

                builder.Property(p => p.ManufacturingPlant)
                       .HasMaxLength(50)
                       .IsRequired();
            });

            // Initial data seed
            modelBuilder.Entity<Truck>().HasData(
                new Truck
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Model = "FH",
                    ManufacturingYear = DateTime.UtcNow.Year,
                    ChassisCode = "FH1234567",
                    Color = "Red",
                    ManufacturingPlant = "Brazil"
                },
                new Truck
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Model = "FM",
                    ManufacturingYear = DateTime.UtcNow.Year,
                    ChassisCode = "FM2345678",
                    Color = "Blue",
                    ManufacturingPlant = "Sweden"
                },
                new Truck
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Model = "VM",
                    ManufacturingYear = DateTime.UtcNow.Year,
                    ChassisCode = "VM3456789",
                    Color = "White",
                    ManufacturingPlant = "United States"
                }
            );
        }
    }
}
