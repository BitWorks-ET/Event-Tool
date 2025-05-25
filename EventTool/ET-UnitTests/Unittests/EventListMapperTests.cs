// Datei: ET-UnitTests/Unittests/EventListMapperTests.cs
using ET_Backend.Models;
using ET_Backend.Services.Mapping;
using ET.Shared.DTOs;
using Xunit;
using System.Collections.Generic;


namespace ET_UnitTests.Unittests
{
    public class EventListMapperTests
    {
        [Fact]
        public void ToDto_WithViewer_SetsOrganizerAndSubscribedCorrectly()
        {
            var viewer = new Account { Id = 1 };
            var evt = new Event
            {
                Id = 42,
                Name = "Testevent",
                Description = "Beschreibung",
                MaxParticipants = 100,
                Participants = new List<Account> { viewer },
                Organizers = new List<Account> { viewer }
            };

            var dto = EventListMapper.ToDto(evt, viewer);

            Assert.Equal(evt.Id, dto.EventId);
            Assert.Equal(evt.Name, dto.Name);
            Assert.Equal(evt.Description, dto.Description);
            Assert.Equal(1, dto.Participants);
            Assert.Equal(evt.MaxParticipants, dto.MaxParticipants);
            Assert.True(dto.IsOrganizer);
            Assert.True(dto.IsSubscribed);
        }

        [Fact]
        public void ToDto_WithoutViewer_SetsOrganizerAndSubscribedFalse()
        {
            var evt = new Event
            {
                Id = 7,
                Name = "Event ohne Bezug",
                Description = "Desc",
                MaxParticipants = 10,
                Participants = new List<Account>(),
                Organizers = new List<Account>()
            };

            var dto = EventListMapper.ToDto(evt);

            Assert.Equal(evt.Id, dto.EventId);
            Assert.Equal(evt.Name, dto.Name);
            Assert.Equal(evt.Description, dto.Description);
            Assert.Equal(0, dto.Participants);
            Assert.Equal(evt.MaxParticipants, dto.MaxParticipants);
            Assert.False(dto.IsOrganizer);
            Assert.False(dto.IsSubscribed);
        }
    }
}