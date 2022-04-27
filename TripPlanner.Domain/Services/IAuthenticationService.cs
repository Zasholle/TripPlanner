using TripPlanner.Domain.Models;

namespace TripPlanner.Domain.Services
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
        Task<RegistrationResult> Register(string email, long phone, string fullName, string username, string password, string confirmPassword);
        Task<User> Login(string username, string password);
    }
}
