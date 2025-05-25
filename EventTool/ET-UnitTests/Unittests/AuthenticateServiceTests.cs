using System.Threading.Tasks;
using Xunit;
using Moq;
using ET_Backend.Models;
using ET_Backend.Repository.Organization;
using ET_Backend.Repository.Person;
using ET_Backend.Repository;
using ET_Backend.Services.Helper.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using FluentResults;
using ET_Backend.Repository.Authentication;
using ET_Backend.Services.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ET_UnitTests.Unittests
{
    public class AuthenticateServiceTests
    {
        [Fact]
        public async Task RegisterUser_SuccessfullyRegistersAndStoresUser()
        {
            // Arrange
            var mockAccountRepo = new Mock<IAccountRepository>();
            var mockUserRepo = new Mock<IUserRepository>();
            var mockOrgRepo = new Mock<IOrganizationRepository>();
            var mockLogger = new Mock<ILogger<AuthenticateService>>();
            var mockTokenRepo = new Mock<IEmailVerificationTokenRepository>();
            var mockEmailService = new Mock<IEMailService>();

            var jwtOptions = Options.Create(new JwtOptions
            {
                Issuer = "TestIssuer",
                Audience = "TestAudience",
                ExpirationTime = 1,
                SecretKey = "supergeheimespasswort1234567890!!"
            });

            string firstname = "Max";
            string lastname = "Mustermann";
            string email = "max@firma.de";
            string password = "geheim";
            string domain = "firma.de";

            // User und Organisation müssen vor den Setups deklariert werden!
            var user = new User { Firstname = firstname, Lastname = lastname, Password = password, Id = 1 };
            var org = new Organization { Id = 1, Name = "Firma", Domain = domain };

            // Account existiert noch nicht
            mockAccountRepo.Setup(r => r.AccountExists(email))
                .ReturnsAsync(Result.Ok(false));

            // Organisation existiert
            mockOrgRepo.Setup(r => r.OrganizationExists(domain))
                .ReturnsAsync(Result.Ok(true));

            // User wird erstellt
            mockUserRepo.Setup(r => r.CreateUser(firstname, lastname, password))
                .ReturnsAsync(Result.Ok(user));

            // Organisation wird geladen
            mockOrgRepo.Setup(r => r.GetOrganization(domain))
                .ReturnsAsync(Result.Ok(org));

            // Account wird erstellt (Referenzprobleme vermeiden!)
            mockAccountRepo.Setup(r => r.CreateAccount(
                    email,
                    It.IsAny<Organization>(),
                    Role.Member,
                    It.IsAny<User>()))
                .ReturnsAsync(Result.Ok(new Account
                {
                    EMail = email,
                    User = user,
                    Organization = org,
                    Role = Role.Member
                }));

            // Token speichern
            mockTokenRepo.Setup(r => r.CreateAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(Result.Ok());

            // E-Mail senden
            mockEmailService.Setup(r => r.SendAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var service = new AuthenticateService(
                mockAccountRepo.Object,
                mockUserRepo.Object,
                mockOrgRepo.Object,
                jwtOptions,
                mockLogger.Object,
                mockTokenRepo.Object,
                mockEmailService.Object
            );

            // Act
            var result = await service.RegisterUser(firstname, lastname, email, password);

            // Assert
            Assert.True(result.IsSuccess, $"Fehler: {string.Join(", ", result.Errors.Select(e => e.Message))}");
            Assert.Equal("Benutzer wurde erfolgreich registriert. Bitte E-Mail bestätigen.", result.Value);

            // Überprüfen, ob die Methoden mit den richtigen Parametern aufgerufen wurden
            mockAccountRepo.Verify(r => r.AccountExists(email), Times.Once);
            mockOrgRepo.Verify(r => r.OrganizationExists(domain), Times.Once);
            mockOrgRepo.Verify(r => r.GetOrganization(domain), Times.Once);
            mockAccountRepo.Verify(r => r.CreateAccount(email, It.IsAny<Organization>(), Role.Member, It.IsAny<User>()), Times.Once);
        }


        [Fact]
        public void GenerateJwtToken_Creates_Valid_Token_With_Claims()
        {
            // Arrange
            var jwtOptions = new JwtOptions
            {
                SecretKey = "supergeheimespasswort1234567890!!", // Mindestens 32 Zeichen für HMACSHA256
                Issuer = "TestIssuer",
                Audience = "TestAudience",
                ExpirationTime = 1,
                BackendBaseUrl = "http://localhost/"
            };

            var optionsMock = new Mock<IOptions<JwtOptions>>();
            optionsMock.Setup(o => o.Value).Returns(jwtOptions);

            var loggerMock = new Mock<ILogger<AuthenticateService>>();

            // Dummy-Repositories (werden nicht benötigt für diesen Test)
            var accountRepo = new Mock<IAccountRepository>();
            var userRepo = new Mock<IUserRepository>();
            var orgRepo = new Mock<IOrganizationRepository>();
            var tokenRepo = new Mock<IEmailVerificationTokenRepository>();
            var mailService = new Mock<IEMailService>();

            var service = new AuthenticateService(
                accountRepo.Object,
                userRepo.Object,
                orgRepo.Object,
                optionsMock.Object,
                loggerMock.Object,
                tokenRepo.Object,
                mailService.Object
            );

            var account = new Account
            {
                Id = 42,
                EMail = "test@example.com",
                IsVerified = true,
                Role = Role.Member,
                User = new User { Firstname = "Max", Lastname = "Mustermann", Password = "pw" },
                Organization = new Organization { Name = "TestOrg", Domain = "test.org", Description = "desc" }
            };

            // Zugriff auf die private Methode über Reflection
            var method = typeof(AuthenticateService).GetMethod("GenerateJwtToken", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Assert.NotNull(method);

            // Act
            var token = (string)method.Invoke(service, new object[] { account });

            // Assert: Token parsen und Claims prüfen
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            Assert.Equal("TestIssuer", jwt.Issuer);
            Assert.Equal("TestAudience", jwt.Audiences.First());
            Assert.Contains(jwt.Claims, c => c.Type == JwtRegisteredClaimNames.Email && c.Value == "test@example.com");
            Assert.Contains(jwt.Claims, c => c.Type == "org" && c.Value == "test.org");
            Assert.Contains(jwt.Claims, c => c.Type == "orgName" && c.Value == "TestOrg");
            Assert.Contains(jwt.Claims, c => c.Type == ClaimTypes.Role && c.Value == "Member");
            Assert.Contains(jwt.Claims, c => c.Type == JwtRegisteredClaimNames.Sub && c.Value == "42");
        }
    }
}
