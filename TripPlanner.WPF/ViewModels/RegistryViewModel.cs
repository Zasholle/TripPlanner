using System.Windows.Input;
using TripPlanner.WPF.Authenticators;
using TripPlanner.WPF.Commands;
using TripPlanner.WPF.Services;

namespace TripPlanner.WPF.ViewModels
{
    public class RegistryViewModel : ViewModelBase
    {
        private string? _username;
        private string? _password;
        private string? _email;
        private long _phone;
        private string? _fullName;
        private string? _confirmPassword;
        public string? Username
        {
            get => _username;
            set
            {
                Set(ref _username, ref value);
                OnPropertyChanged(nameof(CanRegister));
            }
        } 
        public string? Email
        {
            get => _email;
            set
            {
                Set(ref _email, ref value);
                OnPropertyChanged(nameof(CanRegister));
            }

        }
        public long Phone
        {
            get => _phone;
            set
            {
                Set(ref _phone, ref value);
                OnPropertyChanged(nameof(CanRegister));
            }
        }
        public string? FullName
        {
            get => _fullName;
            set
            {
                Set(ref _fullName, ref value);
                OnPropertyChanged(nameof(CanRegister));
            }
        }
        public string? Password
        {
            get => _password;
            set
            {
                Set(ref _password, ref value);
                OnPropertyChanged(nameof(CanRegister));
            }
        }
        public string? ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                Set(ref _confirmPassword, ref value);
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        public bool CanRegister => !string.IsNullOrEmpty(Email) &&
                                   !string.IsNullOrEmpty(Phone.ToString()) &&
                                   !string.IsNullOrEmpty(FullName) &&
                                   !string.IsNullOrEmpty(Username) &&
                                   !string.IsNullOrEmpty(Password) &&
                                   !string.IsNullOrEmpty(ConfirmPassword);

        public MessageViewModel ErrorMessageViewModel { get; }
        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public ICommand NavigateLoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public RegistryViewModel(IAuthenticator authenticator, INavigationService loginNavigationService)
        {
            ErrorMessageViewModel = new MessageViewModel();
            NavigateLoginCommand = new NavigateCommand(loginNavigationService);
            RegisterCommand = new RegisterCommand(this, authenticator, loginNavigationService);
        }
    }
}
