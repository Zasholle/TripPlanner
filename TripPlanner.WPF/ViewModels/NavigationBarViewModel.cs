using System;
using System.Windows.Input;
using TripPlanner.WPF.Commands;
using TripPlanner.WPF.Services;
using TripPlanner.WPF.Stores;

namespace TripPlanner.WPF.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        private readonly UserStore _userStore;
        public ICommand LogoutCommand { get; }

        public bool IsLoggedIn => _userStore.IsLoggedIn;

        public NavigationBarViewModel(UserStore userStore,
            INavigationService loginNavigationService)
        {
            _userStore = userStore;

            LogoutCommand = new LogoutCommand(_userStore, loginNavigationService);

            _userStore.CurrentUserChanged += OnCurrentUserChanged;
        }

        private void OnCurrentUserChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
        }

        public override void Dispose()
        {
            _userStore.CurrentUserChanged -= OnCurrentUserChanged;

            GC.SuppressFinalize(this);
        }
    }
}
