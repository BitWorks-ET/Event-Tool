using Xunit;
using Moq;
using ET_Backend.Services.Helper.Authentication;
using ET_Backend.Repository.Person;
using ET.Shared.DTOs;
using FluentResults;
using System.Threading.Tasks;
using ET_Backend.Repository.Authentication;
using ET_Backend.Repository.Organization;
using ET_Backend.Services.Helper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ET_UnitTests.Unittests
{
    public class AuthenticateServiceLoginTestsWithInvalidInputsShouldReturnOk
    {
        // Mocken 
        private readonly Mock<IAccountRepository> _accountRepoMock;
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IOrganizationRepository> _orgRepoMock;
        private readonly Mock<IEmailVerificationTokenRepository> _tokenRepoMock;
        private readonly Mock<IEMailService> _emailServiceMock;
        private readonly Mock<ILogger<AuthenticateService>> _loggerMock;
        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly AuthenticateService _service;

        public AuthenticateServiceLoginTestsWithInvalidInputsShouldReturnOk()
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
        public async Task Login_Fails_When_Email_Is_Empty()
        {
            var dto = new LoginDto("", "password");
            var result = await _service.LoginUser(dto.EMail, dto.Password);
            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Login_Fails_When_Password_Is_Empty()
        {
            var dto = new LoginDto("test@example.com", "");
            var result = await _service.LoginUser(dto.EMail, dto.Password);
            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Login_Fails_When_Email_Is_Invalid_Format()
        {
            var dto = new LoginDto("notanemail", "password");
            var result = await _service.LoginUser(dto.EMail, dto.Password);
            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Login_Fails_When_Account_Does_Not_Exist()
        {
            // Account existiert nicht wie handelt der Service das?
            _accountRepoMock.Setup(r => r.GetAccount(It.IsAny<string>()))
                .ReturnsAsync(Result.Fail<ET_Backend.Models.Account>("NotFound"));

            var dto = new LoginDto("notfound@example.com", "password");
            var result = await _service.LoginUser(dto.EMail, dto.Password);
            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Login_Fails_When_Password_Is_Wrong()
        {
            var account = new ET_Backend.Models.Account
            {
                EMail = "test@example.com",
                User = new ET_Backend.Models.User { Password = "hashedpassword" }
            };
            _accountRepoMock.Setup(r => r.GetAccount("test@example.com"))
                .ReturnsAsync(Result.Ok(account));

            var dto = new LoginDto("test@example.com", "wrongpassword");
            var result = await _service.LoginUser(dto.EMail, dto.Password);
            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Login_Fails_When_Database_Error()
        {
            _accountRepoMock.Setup(r => r.GetAccount(It.IsAny<string>()))
                .ReturnsAsync(Result.Fail<ET_Backend.Models.Account>("DBError: Connection failed"));

            var dto = new LoginDto("test@example.com", "password");
            var result = await _service.LoginUser(dto.EMail, dto.Password);
            Assert.True(result.IsFailed);
        }
    }
}
