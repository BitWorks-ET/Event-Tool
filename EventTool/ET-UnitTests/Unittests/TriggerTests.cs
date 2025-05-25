using ET_Backend.Models;
using Xunit;

namespace ET_UnitTests.Unittests
{
    public class TriggerTests
    {
        [Fact]
        public void CanCreateAndSetProperties()
        {
            var trigger = new Trigger
            {
                Id = 42,
                Attribute = "TestAttribut"
            };

            Assert.Equal(42, trigger.Id);
            Assert.Equal("TestAttribut", trigger.Attribute);
        }
    }
}
