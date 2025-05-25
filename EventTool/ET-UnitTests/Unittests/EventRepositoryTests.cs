using System;
using System.Data;
using System.Linq;
using Microsoft.Data.Sqlite;
using Dapper;
using ET_Backend.Models;
using ET_Backend.Repository.Event;
using Xunit;
using Xunit.Abstractions;

namespace ET_UnitTests.Unittests
{
    public class EventRepositoryTests
    {
        private readonly ITestOutputHelper _output;

        public EventRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
        }

        private IDbConnection CreateInMemoryDb()
        {
            var conn = new SqliteConnection("Data Source=:memory:");
            conn.Open();

            // TypeHandler manuell registrieren, um DateOnly und TimeOnly zu unterstützen
            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
            SqlMapper.AddTypeHandler(new TimeOnlyTypeHandler());

            // Vollständige Events-Tabelle mit allen benötigten Spalten
            conn.Execute(@"CREATE TABLE Events (
                Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                Name TEXT, 
                Description TEXT,
                OrganizationId INTEGER,
                ProcessId INTEGER NULL,
                StartDate TEXT,
                EndDate TEXT,
                StartTime TEXT,
                EndTime TEXT,
                Location TEXT,
                MinParticipants INTEGER,
                MaxParticipants INTEGER,
                RegistrationStart TEXT,
                RegistrationEnd TEXT,
                IsBlueprint INTEGER
            )");

            conn.Execute("CREATE TABLE Organizations (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Description TEXT, Domain TEXT)");
            return conn;
        }




        [Fact]
        public async Task GetEvent_ReturnsFail_WhenNotExists()
        {
            using var db = CreateInMemoryDb();
            var repo = new EventRepository(db);

            var result = await repo.GetEvent(999);

            Assert.False(result.IsSuccess);
            Assert.Contains("NotFound", result.Errors[0].Message);
        }

        

        [Fact]
        public async Task DeleteEvent_RemovesEvent()
        {
            using var db = CreateInMemoryDb();
            db.Execute(@"INSERT INTO Events (
                Id, Name, OrganizationId, Description, StartDate, EndDate, StartTime, EndTime, Location,
                MinParticipants, MaxParticipants, RegistrationStart, RegistrationEnd, IsBlueprint
            ) VALUES (
                1, 'Event1', 1, 'Beschreibung', '2023-01-01', '2023-01-01', '12:00:00', '13:00:00', 'Ort',
                1, 10, '2022-12-01', '2022-12-31', 0
            )");
            var repo = new EventRepository(db);

            var result = await repo.DeleteEvent(1);

            if (!result.IsSuccess)
                _output.WriteLine($"Fehler: {string.Join(", ", result.Errors.Select(e => e.Message))}");

            Assert.True(result.IsSuccess);

            var count = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Events WHERE Id = 1");
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task DeleteEvent_ReturnsFail_WhenEventNotExists()
        {
            using var db = CreateInMemoryDb();
            var repo = new EventRepository(db);

            var result = await repo.DeleteEvent(999);

            Assert.False(result.IsSuccess);
            Assert.Contains("NotFound", result.Errors[0].Message);
        }

        [Fact]
        public async Task EventExists_ReturnsTrue_WhenExists()
        {
            using var db = CreateInMemoryDb();
            db.Execute(@"INSERT INTO Events (
                Id, Name, OrganizationId, Description, StartDate, EndDate, StartTime, EndTime, Location,
                MinParticipants, MaxParticipants, RegistrationStart, RegistrationEnd, IsBlueprint
            ) VALUES (
                1, 'Event1', 1, 'Beschreibung', '2023-01-01', '2023-01-01', '12:00:00', '13:00:00', 'Ort',
                1, 10, '2022-12-01', '2022-12-31', 0
            )");
            var repo = new EventRepository(db);

            var result = await repo.EventExists(1);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }

        [Fact]
        public async Task EventExists_ReturnsFalse_WhenNotExists()
        {
            using var db = CreateInMemoryDb();
            var repo = new EventRepository(db);

            var result = await repo.EventExists(999);

            Assert.True(result.IsSuccess);
            Assert.False(result.Value);
        }


        [Fact]
        public async Task CreateEvent_ReturnsFail_WhenOrganizationIsNull()
        {
            using var db = CreateInMemoryDb();
            var repo = new EventRepository(db);

            var result = await repo.CreateEvent("TestEvent", null);

            Assert.False(result.IsSuccess);
            Assert.Contains("DBError", result.Errors[0].Message);
        }

        [Fact]
        public async Task GetEventsByOrganizationId_ReturnsEmptyList_WhenNoEvents()
        {
            using var db = CreateInMemoryDb();
            db.Execute("INSERT INTO Organizations (Id, Name, Description) VALUES (1, 'demo.org', 'Beschreibung')");
            var repo = new EventRepository(db);

            var result = await repo.GetEventsByOrganizationId(1);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Empty(result.Value);
        }

        [Fact]
        public async Task EditEvent_UpdatesEventSuccessfully()
        {
            using var db = CreateInMemoryDb();
            db.Execute("INSERT INTO Organizations (Id, Name, Description) VALUES (1, 'demo.org', 'Beschreibung')");
            db.Execute(@"INSERT INTO Events (
        Id, Name, OrganizationId, Description, StartDate, EndDate, StartTime, EndTime, Location,
        MinParticipants, MaxParticipants, RegistrationStart, RegistrationEnd, IsBlueprint
    ) VALUES (
        1, 'Event1', 1, 'Beschreibung', '2023-01-01', '2023-01-01', '12:00:00', '13:00:00', 'Ort',
        1, 10, '2022-12-01', '2022-12-31', 0
    )");
            var repo = new EventRepository(db);

            var org = new Organization { Id = 1, Name = "Org", Description = "Beschreibung" };
            var updatedEvent = new ET_Backend.Models.Event
            {
                Id = 1,
                Name = "UpdatedEvent",
                Description = "Neue Beschreibung",
                Organization = org,
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                StartTime = TimeOnly.FromDateTime(DateTime.Now),
                EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)),
                Location = "Neuer Ort",
                MinParticipants = 2,
                MaxParticipants = 20,
                RegistrationStart = DateOnly.FromDateTime(DateTime.Today),
                RegistrationEnd = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                IsBlueprint = true
            };

            var result = await repo.EditEvent(updatedEvent);

            Assert.True(result.IsSuccess);

            var dbEvent = await repo.GetEvent(1);
            Assert.Equal("UpdatedEvent", dbEvent.Value.Name);
            Assert.Equal("Neue Beschreibung", dbEvent.Value.Description);
            Assert.Equal("Neuer Ort", dbEvent.Value.Location);
            Assert.True(dbEvent.Value.IsBlueprint);
        }

        [Fact]
        public async Task EditEvent_ReturnsFail_WhenEventDoesNotExist()
        {
            using var db = CreateInMemoryDb();
            var repo = new EventRepository(db);

            var org = new Organization { Id = 1, Name = "Org", Description = "Beschreibung" };
            var nonExistingEvent = new ET_Backend.Models.Event
            {
                Id = 999,
                Name = "NichtVorhanden",
                Organization = org,
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                StartTime = TimeOnly.FromDateTime(DateTime.Now),
                EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)),
                Location = "Ort",
                MinParticipants = 1,
                MaxParticipants = 10,
                RegistrationStart = DateOnly.FromDateTime(DateTime.Today),
                RegistrationEnd = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                IsBlueprint = false
            };

            var result = await repo.EditEvent(nonExistingEvent);

            Assert.False(result.IsSuccess);
            Assert.Contains("NotFound", result.Errors[0].Message);
        }



    }
}
