using ET_Backend.Models;
using ET_Backend.Repository.Event;
using ET_Backend.Repository.Organization;
using ET_Backend.Repository.Person;
using ET_Backend.Services.Event;
using FluentResults;
using Moq;
using Xunit;

namespace ET_UnitTests.Unittests
{
    public class EventServiceTests
    {
        [Fact]
        public async Task GetEventsFromOrganization_ReturnsEvents_WhenOrganizationExists()
        {
            // Arrange
            var mockRepo = new Mock<IEventRepository>();
            var mockOrgRepo = new Mock<IOrganizationRepository>();
            var mockAccountRepo = new Mock<IAccountRepository>();

            var events = new List<Event>
            {
                new Event { Id = 1, Name = "Event 1" },
                new Event { Id = 2, Name = "Event 2" }
            };

            mockRepo.Setup(r => r.GetEventsByOrganization(1))
                .ReturnsAsync(Result.Ok(events));

            var service = new EventService(
                mockRepo.Object,
                mockOrgRepo.Object,
                mockAccountRepo.Object);

            // Act
            var result = await service.GetEventsFromOrganization(1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);
            mockRepo.Verify(r => r.GetEventsByOrganization(1), Times.Once);
        }

        [Fact]
        public async Task GetEventsFromOrganization_ReturnsFail_WhenRepositoryFails()
        {
            // Arrange
            var mockRepo = new Mock<IEventRepository>();
            var mockOrgRepo = new Mock<IOrganizationRepository>();
            var mockAccountRepo = new Mock<IAccountRepository>();

            mockRepo.Setup(r => r.GetEventsByOrganization(999))
                .ReturnsAsync(Result.Fail("Organization not found"));

            var service = new EventService(
                mockRepo.Object,
                mockOrgRepo.Object,
                mockAccountRepo.Object);

            // Act
            var result = await service.GetEventsFromOrganization(999);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("Organization not found", result.Errors.First().Message);
        }

        // Hinweis: Weitere Methoden sind als TODO markiert und implementieren derzeit nur Stubs,
        // daher wird hier auf weitere Tests verzichtet
    }
}
