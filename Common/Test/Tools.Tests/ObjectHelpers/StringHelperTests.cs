
using Tools.Hashing;
using Tools.ObjectHelpers;
using Xunit;

namespace Tools.Tests.ObjectHelpers
{
    public class StringHelperTests
    {
        [Theory]
        [InlineData("Alper Basda")]
        public void NotContainSpace_WhenDataContainsSpace_ReturnFalse(string text)
        {
            Assert.False(StringHelper.NotContainSpace(text));
        }

        [Theory]
        [InlineData("AlperBasda")]
        public void NotContainSpace_WhenDataNotContainsSpace_ReturnFalse(string text)
        {
            Assert.True(StringHelper.NotContainSpace(text));
        }

        [Theory]
        [InlineData("AlperBasda@vvdsbd.fdbre")]
        [InlineData("AlperBasda@vvdsbd")]
        [InlineData("AlperBasda")]
        public void IsValidMailAddress_WhenNotValid_ReturnFalse(string text)
        {
            Assert.False(StringHelper.IsValidMailAddress(text));
        }

        [Theory]
        [InlineData("AlperBasda@gmail.com")]
        public void IsValidMailAddress_WhenValid_ReturnTrue(string text)
        {
            Assert.True(StringHelper.IsValidMailAddress(text));
        }

        [Theory]
        [InlineData("12343234ut")]
        [InlineData("+905514327331")]
        [InlineData("555")]
        public void IsValidPhoneNumber_WhenNotValid_ReturnFalse(string text)
        {
            Assert.False(StringHelper.IsValidPhoneNumber(text));
        }

        [Theory]
        [InlineData("5514327331")]
        [InlineData("05514327331")]
        public void IsValidPhoneNumber_WhenValid_ReturnTrue(string text)
        {
            Assert.True(StringHelper.IsValidPhoneNumber(text));
        }

        [Fact]
        public void ToStringContent_WhenDataNull_ReturnNull()
        {
            Assert.Null(StringHelper.ToStringContent<SerializableClass>(null));

        }

        [Fact]
        public void ToStringContent_WhenDataNotNull_ReturnStringContent()
        {
            Assert.NotNull(StringHelper.ToStringContent<SerializableClass>(new SerializableClass { TestClass = "testtes" }));
        }
    }
    class SerializableClass
    {
        public string TestClass { get; set; }
    }
}
