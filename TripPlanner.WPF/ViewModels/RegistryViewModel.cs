using System.Windows.Input;
using TripPlanner.WPF.Commands;
using TripPlanner.WPF.Services;
using TripPlanner.WPF.Stores;

namespace TripPlanner.WPF.ViewModels
{
    public class RegistryViewModel : ViewModelBase
    {
        public ICommand NavigateLoginCommand { get; }

        public RegistryViewModel(NavigationStore navigationStore)
        {
            NavigateLoginCommand = new NavigateCommand<LoginViewModel>(
                new NavigationService<LoginViewModel>(
                    navigationStore, () => new LoginViewModel(navigationStore)));

        }
    }
}
