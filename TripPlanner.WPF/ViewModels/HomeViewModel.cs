﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using TripPlanner.Domain.Models;
using TripPlanner.WPF.Commands;
using TripPlanner.WPF.Services;

namespace TripPlanner.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private ObservableCollection<House>? _houses;

        public ObservableCollection<House>? Houses
        {
            get => _houses;
            set => Set(ref _houses, ref value);
        }

        public ICommand LogOutCommand { get; }
        public ICommand LoadCommand { get; }

        public HomeViewModel(IHouseDataService houseDataService, 
            INavigationService loginNavigationService)
        {
            LogOutCommand = new NavigateCommand(loginNavigationService);
            LoadCommand = new LoadCommand(this, houseDataService);
        }
    }
}
