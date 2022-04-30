using Microsoft.EntityFrameworkCore;

namespace TripPlanner.EntityFramework
{
    public class TripPlannerDbContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public TripPlannerDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        public TripPlannerDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<TripPlannerDbContext>();

            _configureDbContext(options);

            return new TripPlannerDbContext(options.Options);
        }
    }
}
