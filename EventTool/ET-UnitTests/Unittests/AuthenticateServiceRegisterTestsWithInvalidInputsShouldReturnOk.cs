using Xunit;
using Moq;
using ET_Backend.Services.Helper.Authentication;
using ET_Backend.Repository.Person;
using ET_Backend.Repository.Organization;
using ET_Backend.Repository.Authentication;
using ET_Backend.Services.Helper;
using ET.Shared.DTOs;
using FluentResults;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace ET_UnitTests.Unittests
{
    public class AuthenticateServiceRegisterTestsWithInvalidInputsShouldReturnOk
    {
        private readonly Mock<IAccountRepository> _accountRepoMock;
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IOrganizationRepository> _orgRepoMock;
        private readonly Mock<IEmailVerificationTokenRepository> _tokenRepoMock;
        private readonly Mock<IEMailService> _emailServiceMock;
        private readonly Mock<ILogger<AuthenticateService>> _loggerMock;
        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly AuthenticateService _service;

        public AuthenticateServiceRegisterTestsWithInvalidInputsShouldReturnOk()
        {
            _accountRepoMock = new Mock<IAccountRepository>();
            _userRepoMock = new Mock<IUserRepository>();
            _orgRepoMock = new Mock<IOrganizationRepository>();
            _tokenRepoMock = new Mock<IEmailVerificationTokenRepository>();
            _emailServiceMock = new Mock<IEMailService>();
            _loggerMock = new Mock<ILogger<AuthenticateService>>();
            _jwtOptions = Options.Create(new JwtOptions
            {
                Issuer = "TestIssuer",
                Audience = "TestAudience",
                ExpirationTime = 1,
                SecretKey = "SuperSecretKey1234567890",
                FrontendBaseUrl = "http://localhost",
                BackendBaseUrl = "http://localhost"
            });

            _service = new AuthenticateService(
                _accountRepoMock.Object,
                _userRepoMock.Object,
                _orgRepoMock.Object,
                _jwtOptions,
                _loggerMock.Object,
                _tokenRepoMock.Object,
                _emailServiceMock.Object
            );
        }

        [Fact]
        public async Task Register_Fails_When_Firstname_Is_Empty()
        {
            var result = await _service.RegisterUser("", "Mustermann", "test@example.com", "password");
            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Register_Fails_When_Lastname_Is_Empty()
        {
            var result = await _service.RegisterUser("Max", "", "test@example.com", "password");
            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Register_Fails_When_Email_Is_Empty()
        {
            var result = await _service.RegisterUser("Max", "Mustermann", "", "password");
            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Register_Fails_When_Password_Is_Empty()
        {
            var result = await _service.RegisterUser("Max", "Mustermann", "test@example.com", "");
            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Register_Fails_When_Email_Is_Invalid_Format()
        {
            var result = await _service.RegisterUser("Max", "Mustermann", "notanemail", "password");
            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Register_Fails_When_Email_Already_Exists()
        {
            _accountRepoMock.Setup(r => r.AccountExists("test@example.com"))
                .ReturnsAsync(Result.Ok(true));

            var result = await _service.RegisterUser("Max", "Mustermann", "test@example.com", "password");
            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Register_Fails_When_No_Organization_Exists()
        {
            // Mock-AccountRepo wird konfiguriert jedes mal "no organization" zurückkzugeben bei AccountExists und getOrganization
            _accountRepoMock.Setup(r => r.AccountExists(It.IsAny<string>()))
                .ReturnsAsync(Result.Ok(false));
            _orgRepoMock.Setup(r => r.GetOrganization(It.IsAny<string>()))
                .ReturnsAsync(Result.Fail<ET_Backend.Models.Organization>("No organization exists for this E-Mail"));

            var result = await _service.RegisterUser("Max", "Mustermann", "test@example.com", "password");
            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Register_Fails_When_Database_Error()
        {
            // Mock-AccountRepository wird konfiguriert jedes mal DB Error zurückzugeben bei Anfage ob AccountExists
            _accountRepoMock.Setup(r => r.AccountExists(It.IsAny<string>()))
                .ReturnsAsync(Result.Fail<bool>("DBError: Connection failed"));

            var result = await _service.RegisterUser("Max", "Mustermann", "test@example.com", "password");
            Assert.True(result.IsFailed);
        }
    }
}
