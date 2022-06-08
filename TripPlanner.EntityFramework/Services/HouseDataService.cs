using Microsoft.EntityFrameworkCore;
using TripPlanner.Domain.Models;
using TripPlanner.Domain.Services.Data;
using TripPlanner.EntityFramework.Services.Common;

namespace TripPlanner.EntityFramework.Services
{
    public class HouseDataService : IHouseService
    {
        private readonly TripPlannerDbContextFactory _contextFactory;
        private readonly NonQueryDataService<House> _nonQueryDataService;

        public HouseDataService(TripPlannerDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<House>(contextFactory);
        }

        public async Task<House> Create(House entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<House> Update(int id, House entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<House?> GetById(int id)
        {
            await using var context = _contextFactory.CreateDbContext();
            return await context.Houses.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<House>> GetAll()
        {
            await using var context = _contextFactory.CreateDbContext();

            return await context.Houses.ToListAsync();
        }

        public async Task<IEnumerable<House>> GetByFilters(Filters filter)
        {
            await using var context = _contextFactory.CreateDbContext();

            return await context.Houses
                .Where(o => o.Area >= filter.MinArea && o.Area <= filter.MaxArea)
                .Where(o => o.Beds >= filter.MinBeds && o.Beds <= filter.MaxBeds)
                .Where(o => o.Price >= filter.MinPrice && o.Price <= filter.MaxPrice)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetLocationList()
        {
            await using var context = _contextFactory.CreateDbContext();
            return await context.Houses
                .GroupBy(x => x.Locality.Name)
                .Select(x => x.First().Locality.Name)
                .ToListAsync();
        }
    }
}
