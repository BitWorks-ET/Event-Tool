using System.Threading.Tasks;
using ET_Backend.Controllers;
using ET_Backend.Services.Person;
using ET.Shared.DTOs;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ET_UnitTests.Unittests
{
    public class UserControllerTests
    {
        [Fact]
        public async Task UpdateUser_ReturnsOk_WhenSuccess()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var userId = 1;
            var dto = new UserDto("Max", "Mustermann", "max@firma.de");
            mockService.Setup(s => s.UpdateUserAsync(userId, dto))
                .ReturnsAsync(Result.Ok());

            var controller = new UserController(mockService.Object);

            // Act
            var result = await controller.UpdateUser(userId, dto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateUser_ReturnsBadRequest_WhenFailed()
        {
            // Arrange

            // UserService gibt immer Error zurück -> wie reagiert UserController?
            var mockService = new Mock<IUserService>();
            var userId = 1;
            var dto = new UserDto("Max", "Mustermann", "max@firma.de");
            var errors = new[] { new Error("Fehler beim Update") };
            mockService.Setup(s => s.UpdateUserAsync(userId, dto))
                .ReturnsAsync(Result.Fail(errors));

            var controller = new UserController(mockService.Object);

            // Act
            var result = await controller.UpdateUser(userId, dto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(errors, badRequest.Value);
        }

    }
}
