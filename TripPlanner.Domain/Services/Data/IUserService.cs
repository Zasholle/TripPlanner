using TripPlanner.Domain.Models;

namespace TripPlanner.Domain.Services.Data
{
    public interface IUserService : IDataService<User>
    {
        Task<User?> GetByUsername(string username);
        Task<User?> GetByEmail(string email);
    }
}
