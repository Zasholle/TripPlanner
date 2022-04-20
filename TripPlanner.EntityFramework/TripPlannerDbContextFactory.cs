using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TripPlanner.EntityFramework
{
    public class TripPlannerDbContextFactory : IDesignTimeDbContextFactory<TripPlannerDbContext>
    {
        public TripPlannerDbContext CreateDbContext(string[]? args = null)
        {
            var options = new DbContextOptionsBuilder<TripPlannerDbContext>();
            options.UseSqlServer("Server=localhost;Database=TripPlannerDB;Trusted_Connection=True;");

            return new TripPlannerDbContext(options.Options);
        }
    }
}
