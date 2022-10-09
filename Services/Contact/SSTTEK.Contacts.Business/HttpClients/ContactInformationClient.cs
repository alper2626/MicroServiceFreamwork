using EntityBase.Concrete;
using EntityBase.Poco.Responses;
using SSTTEK.Contact.Entities.Poco.ContactInformationDto;
using Tools.ObjectHelpers;

namespace SSTTEK.Contact.Business.HttpClients
{
    public class ContactInformationClient : IContactInformationClient
    {
        private readonly HttpClient _httpClient;

        public ContactInformationClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<ContactInformationResponse>> GetContactInformationAsync(FilterModel request)
        {
            var res = await _httpClient.PostAsync("/api/ContactInformations/get", StringHelper.ToStringContent(request));
            res.EnsureSuccessStatusCode();
            return await HttpContentHelper.ContentToObject<ContactInformationResponse>(res.Content);
        }

        public async Task<Response<List<ContactInformationResponse>>> ListContactInformationAsync(FilterModel request)
        {
            var res = await _httpClient.PostAsync("/api/ContactInformations/list", StringHelper.ToStringContent(request));
            res.EnsureSuccessStatusCode();
            return await HttpContentHelper.ContentToObject<List<ContactInformationResponse>>(res.Content);
        }

        
    }
}
