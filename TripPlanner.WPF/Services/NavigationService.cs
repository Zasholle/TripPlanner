using System;
using TripPlanner.WPF.Stores;
using TripPlanner.WPF.ViewModels;

namespace TripPlanner.WPF.Services
{
    public class NavigationService<T> : INavigationService
        where T : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<T> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<T> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
