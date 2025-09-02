using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TruckLibrary.Core.DataContext
{
    public class TruckLibraryDbContextFactory : IDesignTimeDbContextFactory<TruckLibraryDbContext>
    {
        public TruckLibraryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TruckLibraryDbContext>();
            optionsBuilder.UseSqlite("Data Source=meubanco.db");

            return new TruckLibraryDbContext(optionsBuilder.Options);
        }
    }
}