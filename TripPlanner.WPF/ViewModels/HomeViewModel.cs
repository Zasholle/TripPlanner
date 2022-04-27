using System.Windows.Input;
using TripPlanner.WPF.Commands;
using TripPlanner.WPF.Services;
using TripPlanner.WPF.Stores;

namespace TripPlanner.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public string Username { get; set; }
        public ICommand LogOutCommand { get; }

        public HomeViewModel( NavigationStore navigationStore)
        {
            LogOutCommand = new NavigateCommand<LoginViewModel>(
                new NavigationService<LoginViewModel>(
                    navigationStore, () => new LoginViewModel(navigationStore)));
        }
    }
}
