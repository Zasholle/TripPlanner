using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TripPlanner.Domain.Models;
using TripPlanner.WPF.Commands;
using TripPlanner.WPF.Services;

namespace TripPlanner.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<House>? _houses;
        private IEnumerable<string>? _locations;
        private double _minArea;
        private double _maxArea;
        private int _minBeds;
        private int _maxBeds;
        private double _minPrice;
        private double _maxPrice;

        #endregion

        #region Properties

        public ObservableCollection<House>? Houses
        {
            get => _houses;
            set => Set(ref _houses, ref value);
        }

        public IEnumerable<string>? Locations
        {
            get => _locations;
            set => Set(ref _locations, ref value);
        }

        public double MinArea
        {
            get => _minArea;
            set => Set(ref _minArea, ref value);
        }

        public double MaxArea
        {
            get => _maxArea;
            set => Set(ref _maxArea, ref value);
        }

        public int MinBeds
        {
            get => _minBeds;
            set => Set(ref _minBeds, ref value);
        }

        public int MaxBeds
        {
            get => _maxBeds;
            set => Set(ref _maxBeds, ref value);
        }

        public double MinPrice
        {
            get => _minPrice;
            set => Set(ref _minPrice, ref value);
        }

        public double MaxPrice
        {
            get => _maxPrice;
            set => Set(ref _maxPrice, ref value);
        }

        #endregion

        public ICommand LogOutCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand ClearCommand { get; }

        public HomeViewModel(IHouseDataService houseDataService, 
            INavigationService loginNavigationService)
        {
            LogOutCommand = new NavigateCommand(loginNavigationService);
            LoadCommand = new LoadCommand(this, houseDataService);
            SearchCommand = new SearchCommand(this, houseDataService);
            ClearCommand = new ClearCommand(this, houseDataService);
        }
    }
}
