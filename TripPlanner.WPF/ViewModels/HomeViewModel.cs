using System.Windows.Input;
using TripPlanner.WPF.Commands;
using TripPlanner.WPF.Services;

namespace TripPlanner.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public string Username { get; set; } = null!;

        public ICommand LogOutCommand { get; }

        public HomeViewModel(INavigationService loginNavigationService)
        {
            LogOutCommand = new NavigateCommand(loginNavigationService);
        }
    }
}
