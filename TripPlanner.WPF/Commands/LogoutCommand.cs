using TripPlanner.WPF.Services;
using TripPlanner.WPF.Stores;

namespace TripPlanner.WPF.Commands
{
    public class LogoutCommand : CommandBase
    {
        private readonly INavigationService _navigationService;
        private readonly UserStore _accountStore;

        public LogoutCommand(UserStore accountStore, INavigationService navigationService)
        {
            _accountStore = accountStore;
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            _accountStore.Logout();
            _navigationService.Navigate();
        }
    }
}
