using EntityBase.Poco.Responses;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Tools.ObjectHelpers;
using Xunit;
using static System.Net.Mime.MediaTypeNames;

namespace Tools.Tests.ObjectHelpers
{
    public class HttpContextTests
    {
        [Fact]
        public void ContentToObject_WhenStreamReaded_ReturnObject()
        {
            StringContent content = new StringContent(
                         JsonSerializer.Serialize(Response<NoContent>.Success(200)),
                         Encoding.UTF8,
                         Application.Json);

            Assert.NotNull(HttpContentHelper.ContentToObject<Response<NoContent>>(content));
        }

    }
}
