using System.Security.Claims;
using ET_Backend.Models;
using ET_Backend.Repository.Person;
using ET_Backend.Services.Person;
using ET.Shared.DTOs;
using FluentResults;
using Moq;
using Xunit;

namespace ET_UnitTests.Unittests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task GetCurrentUserAsync_ReturnsAccount_WhenEmailClaimExists()
        {
            // Arrange
            var claims = new[] { new Claim("email", "user@demo.org") };
            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims));
            var account = new Account { EMail = "user@demo.org" };

            var mockAccountRepo = new Mock<IAccountRepository>();
            mockAccountRepo.Setup(r => r.GetAccount("user@demo.org"))
                .ReturnsAsync(Result.Ok(account));

            var service = new UserService(Mock.Of<IUserRepository>(), mockAccountRepo.Object);

            // Act
            var result = await service.GetCurrentUserAsync(principal);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("user@demo.org", result.EMail);
        }

        [Fact]
        public async Task GetCurrentUserAsync_ReturnsNull_WhenNoEmailClaim()
        {
            // Arrange
            var principal = new ClaimsPrincipal(new ClaimsIdentity());
            var service = new UserService(Mock.Of<IUserRepository>(), Mock.Of<IAccountRepository>());

            // Act
            var result = await service.GetCurrentUserAsync(principal);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUserAsync_ReturnsFail_WhenNameEmpty()
        {
            // Arrange
            var mockUserRepo = new Mock<IUserRepository>();
            var service = new UserService(mockUserRepo.Object, Mock.Of<IAccountRepository>());
            var dto = new UserDto("", "", null);

            // Act
            var result = await service.UpdateUserAsync(1, dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Name und Nachname dürfen nicht leer sein.", result.Errors[0].Message);
        }

        [Fact]
        public async Task UpdateUserAsync_ReturnsFail_WhenUserNotFound()
        {
            // Arrange
            var mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(r => r.GetUser(1)).ReturnsAsync(Result.Fail("Benutzer nicht gefunden."));
            var service = new UserService(mockUserRepo.Object, Mock.Of<IAccountRepository>());
            var dto = new UserDto("Max", "Mustermann", null);

            // Act
            var result = await service.UpdateUserAsync(1, dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Benutzer nicht gefunden.", result.Errors[0].Message);
        }

        [Fact]
        public async Task UpdateUserAsync_UpdatesUser_WhenValid()
        {
            // Arrange
            var user = new User { Id = 1, Firstname = "Alt", Lastname = "Alt", Password = "old" };
            var mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(r => r.GetUser(1)).ReturnsAsync(Result.Ok(user));
            mockUserRepo.Setup(r => r.EditUser(It.IsAny<User>())).ReturnsAsync(Result.Ok());

            var service = new UserService(mockUserRepo.Object, Mock.Of<IAccountRepository>());
            var dto = new UserDto("Max", "Mustermann", "neu");

            // Act
            var result = await service.UpdateUserAsync(1, dto);

            // Assert
            Assert.True(result.IsSuccess);
            mockUserRepo.Verify(r => r.EditUser(It.Is<User>(u =>
                u.Firstname == "Max" &&
                u.Lastname == "Mustermann" &&
                u.Password == "neu"
            )), Times.Once);
        }
    }
}
