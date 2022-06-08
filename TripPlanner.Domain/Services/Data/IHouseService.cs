using TripPlanner.Domain.Models;

namespace TripPlanner.Domain.Services.Data
{
    public interface IHouseService : IDataService<House>
    {
        Task<IEnumerable<House>> GetByFilters(Filters filter);
        Task<IEnumerable<string>> GetLocationList();
    }
}
