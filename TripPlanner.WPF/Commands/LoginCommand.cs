using TripPlanner.Domain.Models;
using TripPlanner.WPF.Services;
using TripPlanner.WPF.ViewModels;

namespace TripPlanner.WPF.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly LoginViewModel _viewModel;
        private readonly ParameterNavigationService<User, HomeViewModel> _navigationService;

        public LoginCommand(LoginViewModel viewModel, ParameterNavigationService<User, HomeViewModel> navigationService)
        {
            _viewModel = viewModel;
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            var u = new User();
            _navigationService.Navigate(u);
        }
    }
}
