using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TripPlanner.Domain.Models;
using TripPlanner.Domain.Services.Data;
using TripPlanner.WPF.Stores;
using TripPlanner.WPF.ViewModels;

namespace TripPlanner.WPF.Services
{
    public class HouseService : IHouseDataService
    {
        private readonly IHouseService _houseService;
        private readonly HouseStore _houseStore;

        public HouseService(IHouseService houseService, HouseStore houseStore)
        {
            _houseService = houseService;
            _houseStore = houseStore;
        }

        public ObservableCollection<House>? Houses
        {
            get => _houseStore.Houses;
            private set
            {
                _houseStore.Houses = value;
                StateChanged?.Invoke();
            }
        }

        public event Action? StateChanged;

        public async Task LoadData()
        {
            Houses = new ObservableCollection<House>(await _houseService.GetAll());
        }
    }
}
