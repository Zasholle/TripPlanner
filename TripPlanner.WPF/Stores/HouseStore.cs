using System;
using System.Collections.ObjectModel;
using TripPlanner.Domain.Models;

namespace TripPlanner.WPF.Stores
{
    public class HouseStore
    {
        private ObservableCollection<House>? _houses;

        public ObservableCollection<House>? Houses
        {
            get => _houses;
            set
            {
                _houses = value;
                OnHousesChanged?.Invoke();
            }
        }

        public event Action? OnHousesChanged;
    }
}
