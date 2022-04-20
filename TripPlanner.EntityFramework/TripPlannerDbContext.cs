using Microsoft.EntityFrameworkCore;
using TripPlanner.Domain.Models;

namespace TripPlanner.EntityFramework
{
    public class TripPlannerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<House> Houses { get; set; } = null!;
        public DbSet<Owner> Owners { get; set; } = null!;

        public TripPlannerDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<House>().OwnsOne(o => o.Locality);
            base.OnModelCreating(modelBuilder);
        }
    }
}
