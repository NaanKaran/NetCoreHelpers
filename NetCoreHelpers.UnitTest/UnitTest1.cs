using System;
using Xunit;

namespace NetCoreHelpers.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void DateTimeExtension_ToIST_ReturnTrue()
        {

            var dateTime = DateTime.UtcNow.ToIST();

            Assert.Equal(DateTime.Now.Hour, dateTime.Hour);
        }
    }
}
