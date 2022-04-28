using Microsoft.AspNetCore.Identity;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Models;
using PasswordVerificationResult = Microsoft.AspNetCore.Identity.PasswordVerificationResult;

namespace TripPlanner.Domain.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthenticationService(IUserService userService, IPasswordHasher<User> passwordHasher)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
        }

        public async Task<RegistrationResult> Register(string email, long phone, string fullName, string username, string password, string confirmPassword)
        {
            var result = RegistrationResult.Success;

            if (password != confirmPassword)
            {
                result = RegistrationResult.PasswordsDoNotMatch;
            }

            var emailUser = await _userService.GetByEmail(email);
            
            if (emailUser != null)
            {
                result = RegistrationResult.EmailAlreadyExists;
            }

            var usernameUser = await _userService.GetByUsername(username);

            if (usernameUser != null)
            {
                result = RegistrationResult.UsernameAlreadyExists;
            }

            if (result == RegistrationResult.Success)
            {
                var user = new User
                {
                    Email = email,
                    Username = username,
                    Phone = phone,
                    FullName = fullName
                };

                user.Password = _passwordHasher.HashPassword(user, password);

                await _userService.Create(user);
            }

            return result;
        }

        public async Task<User> Login(string username, string password)
        {
            var storedUser = await _userService.GetByUsername(username);

            if (storedUser == null)
            {
                throw new UserNotFoundException(username);
            }

            var passwordResult = _passwordHasher.VerifyHashedPassword(storedUser, storedUser.Password, password);

            if (passwordResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException(username, password);
            }

            return storedUser;
        }
    }
}
