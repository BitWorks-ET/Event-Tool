using System.Threading.Tasks;
using ET_Backend.Models;
using ET_Backend.Repository.Person;
using ET_Backend.Services.Person;
using FluentResults;
using Moq;
using Xunit;

namespace ET_UnitTests.Unittests
{
    public class AccountServiceTests
    {
        [Fact]
        public async Task GetAccountByMail_ReturnsAccount_WhenFound()
        {
            // Arrange
            var mockRepo = new Mock<IAccountRepository>();
            var account = new Account { EMail = "test@demo.org" };
            mockRepo.Setup(r => r.GetAccount("test@demo.org"))
                .ReturnsAsync(Result.Ok(account));

            var service = new AccountService(mockRepo.Object);

            // Act
            var result = await service.GetAccountByMail("test@demo.org");

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(account, result.Value);
        }

        [Fact]
        public async Task GetAccountByMail_ReturnsFail_WhenNotFound()
        {
            // Arrange
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(r => r.GetAccount("notfound@demo.org"))
                .ReturnsAsync(Result.Fail("Not found"));

            var service = new AccountService(mockRepo.Object);

            // Act
            var result = await service.GetAccountByMail("notfound@demo.org");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Not found", result.Errors[0].Message);
        }
    }
}
