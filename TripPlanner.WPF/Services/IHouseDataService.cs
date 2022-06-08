using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TripPlanner.Domain.Models;

namespace TripPlanner.WPF.Services
{
    public interface IHouseDataService
    {
        ObservableCollection<House>? Houses { get; }
        IEnumerable<string>? Locations { get; }
        event Action StateChanged;
        Task LoadData();
        Task GetByFilters(Filters filter);
        Task GetLocationList();
    }
}
