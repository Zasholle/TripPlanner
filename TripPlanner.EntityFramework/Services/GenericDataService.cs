using Microsoft.EntityFrameworkCore;
using TripPlanner.Domain.Models;
using TripPlanner.Domain.Services;

namespace TripPlanner.EntityFramework.Services
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {
        private readonly TripPlannerDbContextFactory _contextFactory;

        public GenericDataService(TripPlannerDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Create(T entity)
        {
            await using var context = _contextFactory.CreateDbContext();

            var createdEntity = await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();

            return createdEntity.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            await using var context = _contextFactory.CreateDbContext();

            var entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

            if (entity != null)
                context.Set<T>().Remove(entity);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<T> GetById(int id)
        {
            await using var context = _contextFactory.CreateDbContext();

            var entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

            return entity!;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            await using var context = _contextFactory.CreateDbContext();

            var entities = await context.Set<T>().ToListAsync();

            return entities;
        }

        public async Task<T> Update(int id, T entity)
        {
            await using var context = _contextFactory.CreateDbContext();
            entity.Id = id;
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();

            return entity;
        }
    }
}
