using System.Windows.Input;
using TripPlanner.WPF.Commands;
using TripPlanner.WPF.Services;

namespace TripPlanner.WPF.ViewModels
{
    public class RegistryViewModel : ViewModelBase
    {
        public ICommand NavigateLoginCommand { get; }

        public RegistryViewModel(INavigationService loginNavigationService)
        {
            NavigateLoginCommand = new NavigateCommand(loginNavigationService);
        }
    }
}
