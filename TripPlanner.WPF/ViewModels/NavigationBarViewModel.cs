using System.Windows.Input;
using TripPlanner.WPF.Commands;
using TripPlanner.WPF.Services;

namespace TripPlanner.WPF.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateRegistryCommand { get; }
        public ICommand NavigateLoginCommand { get; }

        public NavigationBarViewModel(
            INavigationService homeNavigationService,
            INavigationService registryNavigationService,
            INavigationService loginNavigationService)
        {
            NavigateHomeCommand = new NavigateCommand(homeNavigationService);
            NavigateRegistryCommand = new NavigateCommand(registryNavigationService);
            NavigateLoginCommand = new NavigateCommand(loginNavigationService);
        }
    }
}
