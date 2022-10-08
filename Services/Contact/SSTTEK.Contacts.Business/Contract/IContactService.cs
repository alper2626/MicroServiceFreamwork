using EntityBase.Concrete;
using EntityBase.Poco.Responses;
using SSTTEK.Contact.Entities.Poco.ContactDto;

namespace SSTTEK.Contact.Business.Contract
{
    public interface IContactService
    {
        Task<Response<CreateContactRequest>> Create(CreateContactRequest request);

        Task<Response<UpdateContactRequest>> Update(UpdateContactRequest request);

        Task<Response<ContactResponse>> Get(FilterModel request);

        Task<Response<ContactDetailedResponse>> GetWithDetail(FilterModel request);

        Task<Response<ContactResponse>> Remove(FilterModel request);

        Task<Response<ContactResponse>> Delete(FilterModel request);
    }
}
