using System.Windows.Input;
using TripPlanner.WPF.Commands;
using TripPlanner.WPF.Services;

namespace TripPlanner.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel(INavigationService homeNavigationService,
            INavigationService registryNavigationService)
        {
            LoginCommand = new NavigateCommand(homeNavigationService);
            RegisterCommand = new NavigateCommand(registryNavigationService);
        }
    }
}
