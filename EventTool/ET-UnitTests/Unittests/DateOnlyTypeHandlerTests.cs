using System;
using System.Data;
using Xunit;
using ET_Backend.Repository.Event;


namespace ET_UnitTests.Unittests
{
    public class DateOnlyTypeHandlerTests
    {
        private readonly DateOnlyTypeHandler _handler = new();

        [Theory]
        [InlineData("2025-05-25 10:00:00", 2025, 5, 25)]
        [InlineData("2023-01-01", 2023, 1, 1)]
        public void Parse_StringInput_ReturnsExpectedDateOnly(string input, int expectedYear, int expectedMonth, int expectedDay)
        {
            // Act
            var result = _handler.Parse(input);

            // Assert
            Assert.Equal(expectedYear, result.Year);
            Assert.Equal(expectedMonth, result.Month);
            Assert.Equal(expectedDay, result.Day);
        }

        [Fact]
        public void Parse_DateTimeInput_ReturnsDateOnly()
        {
            var dt = new DateTime(2025, 5, 25, 8, 30, 0);

            var result = _handler.Parse(dt);

            Assert.Equal(2025, result.Year);
            Assert.Equal(5, result.Month);
            Assert.Equal(25, result.Day);
        }

        [Fact]
        public void Parse_LongTicksInput_ReturnsDateOnly()
        {
            var ticks = new DateTime(2022, 12, 31).Ticks;

            var result = _handler.Parse(ticks);

            Assert.Equal(2022, result.Year);
            Assert.Equal(12, result.Month);
            Assert.Equal(31, result.Day);
        }

        [Fact]
        public void Parse_UnsupportedType_ThrowsDataException()
        {
            Assert.Throws<DataException>(() => _handler.Parse(3.14));
        }

    }
}
