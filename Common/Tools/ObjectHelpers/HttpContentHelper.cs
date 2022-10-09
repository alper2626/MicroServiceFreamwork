using EntityBase.Poco.Responses;
using Newtonsoft.Json;

namespace Tools.ObjectHelpers
{
    public static class HttpContentHelper
    {
        public static async Task<Response<T>> ContentToObject<T>(HttpContent content)
            where T : class, new()
        {
            var str = await content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(str))
                return null;
            return JsonConvert.DeserializeObject<Response<T>>(str);
        }
    }
}
