using System;
using System.ComponentModel;
using System.Threading.Tasks;
using TripPlanner.Domain.Services.Authentication;
using TripPlanner.WPF.Authenticators;
using TripPlanner.WPF.Services;
using TripPlanner.WPF.ViewModels;

namespace TripPlanner.WPF.Commands
{
    public class RegisterCommand : AsyncCommandBase
    {
        private readonly RegistryViewModel _registerViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly INavigationService _navigationService;

        public RegisterCommand(RegistryViewModel registerViewModel, IAuthenticator authenticator, INavigationService navigationService)
        {
            _authenticator = authenticator;
            _registerViewModel = registerViewModel;
            _navigationService = navigationService;

            _registerViewModel.PropertyChanged += RegisterViewModel_PropertyChanged!;
        }

        public override bool CanExecute(object? parameter)
        {
            return _registerViewModel.CanRegister && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _registerViewModel.ErrorMessage = string.Empty;

            try
            {
                var registrationResult = await _authenticator.Register(
                    _registerViewModel.Email,
                    _registerViewModel.Phone,
                    _registerViewModel.FullName,
                    _registerViewModel.Username,
                    _registerViewModel.Password,
                    _registerViewModel.ConfirmPassword);

                switch (registrationResult)
                {
                    case RegistrationResult.Success:
                        _navigationService.Navigate();
                        break;
                    case RegistrationResult.PasswordsDoNotMatch:
                        _registerViewModel.ErrorMessage = "Password does not match confirm password.";
                        break;
                    case RegistrationResult.EmailAlreadyExists:
                        _registerViewModel.ErrorMessage = "An account for this email already exists.";
                        break;
                    case RegistrationResult.UsernameAlreadyExists:
                        _registerViewModel.ErrorMessage = "An account for this username already exists.";
                        break;
                    default:
                        _registerViewModel.ErrorMessage = "Registration failed.";
                        break;
                }
            }
            catch (Exception)
            {
                _registerViewModel.ErrorMessage = "Registration failed.";
            }
        }

        private void RegisterViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_registerViewModel.CanRegister))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
