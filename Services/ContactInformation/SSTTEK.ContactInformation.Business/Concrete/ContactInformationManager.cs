using EntityBase.Concrete;
using EntityBase.Poco.Responses;
using SSTTEK.ContactInformation.Business.Contracts;
using SSTTEK.ContactInformation.Entities.Poco.ContactInformationDto;

namespace SSTTEK.ContactInformation.Business.Concrete
{
    public class ContactInformationManager : IContactInformationService
    {
        public Task<Response<CreateContactInformationRequest>> Create(CreateContactInformationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ContactInformationResponse>> Get(FilterModel request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ContactInformationResponse>> Remove(FilterModel request)
        {
            throw new NotImplementedException();
        }
    }
}
