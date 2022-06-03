using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Models;
using TripPlanner.Domain.Services.Authentication;
using TripPlanner.Domain.Services.Data;

namespace TripPlanner.Domain.Tests.Services.AuthenticationServices
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private Mock<IPasswordHasher<User>> _mockPasswordHasher = null!;
        private Mock<IUserService> _mockAccountService = null!;
        private AuthenticationService _authenticationService = null!;

        [SetUp]
        public void SetUp()
        {
            _mockPasswordHasher = new Mock<IPasswordHasher<User>>();
            _mockAccountService = new Mock<IUserService>();
            _authenticationService = new AuthenticationService(_mockAccountService.Object, _mockPasswordHasher.Object);
        }

        [Test]
        public async Task Login_WithCorrectPasswordForExistingUsername_ReturnsAccountForCorrectUsername()
        {
            const string expectedUsername = "testuser";
            const string password = "testpassword";
            User user;
            _mockAccountService.Setup(s => s.GetByUsername(expectedUsername)).ReturnsAsync(user = new User { Username = expectedUsername  });
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(user,It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Success);

            var user1 = await _authenticationService.Login(expectedUsername, password);

            var actualUsername = user1.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithIncorrectPasswordForExistingUsername_ThrowsInvalidPasswordExceptionForUsername()
        {
            const string expectedUsername = "testuser";
            const string password = "testpassword";
            User user;
            _mockAccountService.Setup(s => s.GetByUsername(expectedUsername)).ReturnsAsync(user = new User { Username = expectedUsername });
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(user, It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            var exception = Assert.ThrowsAsync<InvalidPasswordException>(() => _authenticationService.Login(expectedUsername, password));

            var actualUsername = exception!.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithNonExistingUsername_ThrowsInvalidPasswordExceptionForUsername()
        {
            const string expectedUsername = "testuser";
            const string password = "testpassword";
            var user = new User { Username = expectedUsername };
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(user, It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            var exception = Assert.ThrowsAsync<UserNotFoundException>(() => _authenticationService.Login(expectedUsername, password))!;

            var actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public async Task Register_WithPasswordsNotMatching_ReturnsPasswordsDoNotMatch()
        {
            const string password = "testpassword";
            const string confirmPassword = "confirmtestpassword";
            const RegistrationResult expected = RegistrationResult.PasswordsDoNotMatch;

            var actual = await _authenticationService.Register(
                It.IsAny<string>(), It.IsAny<long>(), It.IsAny<string>(), 
                It.IsAny<string>(), password, confirmPassword);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithAlreadyExistingEmail_ReturnsEmailAlreadyExists()
        {
            const string email = "test@gmail.com";
            _mockAccountService.Setup(s => s.GetByEmail(email)).ReturnsAsync(new User());
            const RegistrationResult expected = RegistrationResult.EmailAlreadyExists;

            var actual = await _authenticationService.Register(
                email, It.IsAny<long>(), It.IsAny<string>(), 
                It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>());

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithAlreadyExistingUsername_ReturnsUsernameAlreadyExists()
        {
            const string username = "testuser";
            _mockAccountService.Setup(s => s.GetByUsername(username)).ReturnsAsync(new User());
            const RegistrationResult expected = RegistrationResult.UsernameAlreadyExists;

            var actual = await _authenticationService.Register(
                It.IsAny<string>(), It.IsAny<long>(), It.IsAny<string>(),
                username, It.IsAny<string>(),
                It.IsAny<string>());

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithNonExistingUserAndMatchingPasswords_ReturnsSuccess()
        {
            const RegistrationResult expected = RegistrationResult.Success;

            var actual = await _authenticationService.Register(
                It.IsAny<string>(), It.IsAny<long>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>());

            Assert.AreEqual(expected, actual);
        }
    }
}
