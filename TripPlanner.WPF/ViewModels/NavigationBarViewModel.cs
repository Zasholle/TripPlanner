using System.Windows.Input;
using TripPlanner.WPF.Commands;
using TripPlanner.WPF.Services;
using TripPlanner.WPF.Stores;

namespace TripPlanner.WPF.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        private readonly UserStore _userStore;
        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateRegistryCommand { get; }
        public ICommand NavigateLoginCommand { get; }
        public ICommand LogoutCommand { get; }

        public bool IsLoggedIn => _userStore.IsLoggedIn;

        public NavigationBarViewModel(UserStore userStore,
            INavigationService homeNavigationService,
            INavigationService registryNavigationService,
            INavigationService loginNavigationService)
        {
            _userStore = userStore;
            NavigateHomeCommand = new NavigateCommand(homeNavigationService);
            NavigateRegistryCommand = new NavigateCommand(registryNavigationService);
            NavigateLoginCommand = new NavigateCommand(loginNavigationService);
            LogoutCommand = new LogoutCommand(_userStore);

            _userStore.CurrentUserChanged += OnCurrentUserChanged;
        }

        private void OnCurrentUserChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
        }

        public override void Dispose()
        {
            _userStore.CurrentUserChanged -= OnCurrentUserChanged;

            base.Dispose();
        }
    }
}
