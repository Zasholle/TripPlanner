using TripPlanner.Domain.Models;
using TripPlanner.WPF.Services;
using TripPlanner.WPF.ViewModels;

namespace TripPlanner.WPF.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly ParameterNavigationService<User, HomeViewModel> _navigationService;

        public LoginCommand(ParameterNavigationService<User, HomeViewModel> navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            var u = new User();
            _navigationService.Navigate(u);
        }
    }
}
