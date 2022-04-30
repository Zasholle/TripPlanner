using System;
using System.Threading.Tasks;
using TripPlanner.Domain.Models;
using TripPlanner.Domain.Services.Authentication;
using TripPlanner.WPF.Stores;

namespace TripPlanner.WPF.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly UserStore _userStore;

        public Authenticator(IAuthenticationService authenticationService, UserStore userStore)
        {
            _authenticationService = authenticationService;
            _userStore = userStore;
        }

        public User? CurrentUser
        {
            get => _userStore.CurrentUser;
            private set
            {
                _userStore.CurrentUser = value;
                StateChanged?.Invoke();
            }
        }

        public bool IsLoggedIn => true;

        public event Action? StateChanged;

        public async Task Login(string username, string password)
        {
            CurrentUser = await _authenticationService.Login(username, password);
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        public async Task<RegistrationResult> Register(string email, long phone, string fullName, string username, string password, string confirmPassword)
        {
            return await _authenticationService.Register(email, phone, fullName, username, password, confirmPassword);
        }
    }
}
