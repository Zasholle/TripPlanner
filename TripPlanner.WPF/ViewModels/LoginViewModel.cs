using System.Windows.Input;
using TripPlanner.WPF.Commands;
using TripPlanner.WPF.Services;
using TripPlanner.WPF.Stores;

namespace TripPlanner.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel(NavigationStore navigationStore)
        {
            LoginCommand = new NavigateCommand<HomeViewModel>(
                new NavigationService<HomeViewModel>(
                    navigationStore, () => new HomeViewModel(navigationStore)));

            RegisterCommand = new NavigateCommand<RegistryViewModel>(
                new NavigationService<RegistryViewModel>(
                    navigationStore, () => new RegistryViewModel(navigationStore)));
        }
    }
}
