using Microsoft.EntityFrameworkCore;
using TripPlanner.Domain.Models;
using TripPlanner.Domain.Services.Data;
using TripPlanner.EntityFramework.Services.Common;

namespace TripPlanner.EntityFramework.Services
{
    public class UserDataService : IUserService
    {
        private readonly TripPlannerDbContextFactory _contextFactory;
        private readonly NonQueryDataService<User> _nonQueryDataService;

        public UserDataService(TripPlannerDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<User>(contextFactory);
        }

        public async Task<User> Create(User entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<User> Update(int id, User entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<User> GetById(int id)
        {
            await using var context = _contextFactory.CreateDbContext();

            var entity = 
                context.Users.FirstOrDefault((e) => e.Id == id)!;
            
            return entity;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            await using var context = _contextFactory.CreateDbContext();

            var entities = await context.Users.ToListAsync();

            return entities;
        }

        public async Task<User?> GetByUsername(string username)
        {
            await using var context = _contextFactory.CreateDbContext();

            return await context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetByEmail(string email)
        {
            await using var context = _contextFactory.CreateDbContext();

            return await context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}
