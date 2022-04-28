using System;
using TripPlanner.Domain.Models;

namespace TripPlanner.WPF.Stores
{
    public class UserStore
    {
        private User _currentUser;

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                CurrentUserChanged?.Invoke();
            }
        }

        public bool IsLoggedIn => CurrentUser != null;

        public event Action CurrentUserChanged;

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
