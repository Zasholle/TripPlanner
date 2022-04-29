using TripPlanner.Domain.Models;

namespace TripPlanner.Domain.Services.Authentication
{
    public enum RegistrationResult
    {
        Success,
        PasswordsDoNotMatch,
        EmailAlreadyExists,
        UsernameAlreadyExists
    }

    public interface IAuthenticationService
    {
        /// <summary>
        /// Register a new User.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="phone">The user's phone number.</param>
        /// <param name="fullName">The user's full name.</param>
        /// <param name="username">The user's username.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="confirmPassword">The user's confirmed password.</param>
        /// <returns>The result of the registration.</returns>
        /// <exception cref="Exception">Thrown exception if the registration fails.</exception>
        Task<RegistrationResult> Register(string email, long phone, string fullName, string username, string password, string confirmPassword);

        /// <summary>
        /// Get an User class object for a user's credentials.
        /// </summary>
        /// <param name="username">The existing user's username</param>
        /// <param name="password">The existing user's password</param>
        /// <returns>The existing User from database.</returns>
        /// <exception cref="Exceptions.UserNotFoundException">Thrown if the user doesn't exist.</exception>
        /// <exception cref="Exceptions.InvalidPasswordException">Thrown if the password is invalid.</exception>
        /// <exception cref="Exception">Thrown if the login fails.</exception>
        Task<User> Login(string username, string password);
    }
}
