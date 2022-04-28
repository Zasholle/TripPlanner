using System;
using TripPlanner.WPF.Stores;
using TripPlanner.WPF.ViewModels;

namespace TripPlanner.WPF.Services
{
    public class LayoutNavigationService<T> : INavigationService
    where T : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<T> _createViewModel;
        private readonly Func<NavigationBarViewModel> _createNavigationBarViewModel;

        public LayoutNavigationService(
            NavigationStore navigationStore, 
            Func<T> createViewModel, 
            Func<NavigationBarViewModel> createNavigationBarViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
            _createNavigationBarViewModel = createNavigationBarViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = new LayoutViewModel(_createNavigationBarViewModel(), _createViewModel());
        }
    }
}
