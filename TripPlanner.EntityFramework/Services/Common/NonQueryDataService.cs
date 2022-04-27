using Microsoft.EntityFrameworkCore;
using TripPlanner.Domain.Models;

namespace TripPlanner.EntityFramework.Services.Common
{
    public class NonQueryDataService<T> where T : DomainObject
    {
        private readonly TripPlannerDbContextFactory _contextFactory;

        public NonQueryDataService(TripPlannerDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Create(T entity)
        {
            await using var context = _contextFactory.CreateDbContext();
            var createdResult = await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();

            return createdResult.Entity;
        }

        public async Task<T> Update(int id, T entity)
        {
            await using var context = _contextFactory.CreateDbContext();
            entity.Id = id;

            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            await using var context = _contextFactory.CreateDbContext();

            var entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
            context.Set<T>().Remove(entity!);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
