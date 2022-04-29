using System.Windows.Input;
using TripPlanner.WPF.Authenticators;
using TripPlanner.WPF.Commands;
using TripPlanner.WPF.Services;

namespace TripPlanner.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string? _username;
        private string? _password;

        public string? Username
        {
            get => _username;
            set
            {
                Set(ref _username, ref value);
                OnPropertyChanged(nameof(CanLogin));
            }
        }

        public string? Password
        {
            get => _password;
            set
            {
                Set(ref _password, ref value);
                OnPropertyChanged(nameof(CanLogin));
            }
        }

        public bool CanLogin => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        public MessageViewModel ErrorMessageViewModel { get; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel(IAuthenticator authenticator,
            INavigationService homeNavigationService,
            INavigationService registryNavigationService)
        {
            ErrorMessageViewModel = new MessageViewModel();
            LoginCommand = new LoginCommand(this, authenticator, homeNavigationService);
            RegisterCommand = new NavigateCommand(registryNavigationService);
        }
    }
}
