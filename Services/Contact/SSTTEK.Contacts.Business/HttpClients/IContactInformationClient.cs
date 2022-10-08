using EntityBase.Concrete;
using EntityBase.Poco.Responses;
using SSTTEK.Contact.Entities.Poco.ContactInformationDto;

namespace SSTTEK.Contact.Business.HttpClients
{
    public interface IContactInformationClient
    {
        Task<Response<ContactInformationResponse>> GetContactInformationAsync(FilterModel request);

        Task<Response<List<ContactInformationResponse>>> ListContactInformationAsync(FilterModel request);
    }
}
