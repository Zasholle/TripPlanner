using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TripPlanner.Domain.Models;

namespace TripPlanner.WPF.Stores
{
    public class HouseStore
    {
        private ObservableCollection<House>? _houses;
        private IEnumerable<string>? _locations;

        public ObservableCollection<House>? Houses
        {
            get => _houses;
            set
            {
                _houses = value;
                OnHousesChanged?.Invoke();
            }
        }

        public IEnumerable<string>? Locations
        {
            get => _locations;
            set
            {
                _locations = value;
                OnHousesChanged?.Invoke();
            }
        }

        public event Action? OnHousesChanged;
    }
}
