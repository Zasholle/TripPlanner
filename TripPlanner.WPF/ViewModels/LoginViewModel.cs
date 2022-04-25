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
        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateRegisterCommand { get; }

        public LoginViewModel(NavigationStore navigationStore)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(
                new NavigationService<HomeViewModel>(
                    navigationStore, () => new HomeViewModel(navigationStore)));

            NavigateRegisterCommand = new NavigateCommand<RegistryViewModel>(
                new NavigationService<RegistryViewModel>(
                    navigationStore, () => new RegistryViewModel(navigationStore)));
        }
    }
}
