using EntityBase.Poco.Responses;
using System.Text.Json;

namespace Tools.ObjectHelpers
{
    public static class HttpContentHelper
    {
        public static async Task<Response<T>> ContentToObject<T>(HttpContent content)
            where T: class ,new()
        {
            using var stream =
            await content.ReadAsStreamAsync();

            if (stream == null)
                return null;

            return await JsonSerializer.DeserializeAsync<Response<T>>(stream);
        }
    }
}
