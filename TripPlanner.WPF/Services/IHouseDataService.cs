using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TripPlanner.Domain.Models;

namespace TripPlanner.WPF.Services
{
    public interface IHouseDataService
    {
        ObservableCollection<House>? Houses { get; }
        event Action StateChanged;
        Task LoadData();
    }
}
