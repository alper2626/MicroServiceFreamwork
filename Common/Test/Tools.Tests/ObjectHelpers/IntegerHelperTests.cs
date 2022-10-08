using Tools.ObjectHelpers;
using Xunit;

namespace Tools.Tests.ObjectHelpers
{
    public class IntegerHelperTests
    {
        [Theory]
        [InlineData(5)]
        [InlineData(2)]
        public void CreateNumber_LengthTest(int length)
        {
            var calculated = IntegerHelper.CreateNumber(length);
            Assert.Equal(length, calculated.ToString().Length);
        }
    }
}
