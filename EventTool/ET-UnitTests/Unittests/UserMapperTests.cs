// Datei: ET-UnitTests/Unittests/UserMapperTests.cs
using ET_Backend.Models;
using ET_Backend.Services.Mapping;
using ET.Shared.DTOs;
using Xunit;


namespace ET_UnitTests.Unittests
{
    public class UserMapperTests
    {
        [Fact]
        public void ToModel_MapsDtoToUserModel()
        {
            var dto = new UserDto("Max", "Mustermann", "pw123");
            var user = UserMapper.ToModel(dto, 5);

            Assert.Equal(5, user.Id);
            Assert.Equal(dto.FirstName, user.Firstname);
            Assert.Equal(dto.LastName, user.Lastname);
            Assert.Equal(dto.Password, user.Password);
        }

        [Fact]
        public void ToDto_MapsUserModelToDto()
        {
            var user = new User
            {
                Id = 3,
                Firstname = "Anna",
                Lastname = "Musterfrau",
                Password = "secret"
            };

            var dto = UserMapper.ToDto(user);

            Assert.Equal(user.Firstname, dto.FirstName);
            Assert.Equal(user.Lastname, dto.LastName);
            Assert.Equal(user.Password, dto.Password);
        }
    }
}