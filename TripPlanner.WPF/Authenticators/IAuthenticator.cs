using System;
using System.Threading.Tasks;
using TripPlanner.Domain.Models;
using TripPlanner.Domain.Services.Authentication;

namespace TripPlanner.WPF.Authenticators
{
    public interface IAuthenticator
    {
        User? CurrentUser { get; }
        bool IsLoggedIn { get; }

        event Action StateChanged;

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="phone">The user's phone number.</param>
        /// <param name="fullName">The user's full name.</param>
        /// <param name="username">The user's username.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="confirmPassword">The user's confirmed password.</param>
        /// <returns>The result of the registration.</returns>
        /// <exception cref="Exception">Thrown if the registration fails.</exception>
        Task<RegistrationResult> Register(string email, long phone, string fullName, string username, string password, string confirmPassword);

        /// <summary>
        /// Login to the application.
        /// </summary>
        /// <param name="username">The user's name.</param>
        /// <param name="password">The user's password.</param>
        /// <exception cref="Domain.Exceptions.UserNotFoundException">Thrown if the user does not exist.</exception>
        /// <exception cref="Domain.Exceptions.InvalidPasswordException">Thrown if the password is invalid.</exception>
        /// <exception cref="Exception">Thrown if the login fails.</exception>
        Task Login(string username, string password);
        void Logout();
    }
}
