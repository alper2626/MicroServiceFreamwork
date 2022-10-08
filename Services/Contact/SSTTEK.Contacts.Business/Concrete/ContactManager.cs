using EntityBase.Concrete;
using EntityBase.Poco.Responses;
using SSTTEK.Contact.Business.Contract;
using SSTTEK.Contact.Entities.Poco.ContactDto;

namespace SSTTEK.Contact.Business.Concrete
{
    public class ContactManager : IContactService
    {
        public Task<Response<CreateContactRequest>> Create(CreateContactRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ContactResponse>> Delete(FilterModel request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ContactResponse>> Get(FilterModel request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ContactDetailedResponse>> GetWithDetail(FilterModel request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ContactResponse>> Remove(FilterModel request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<UpdateContactRequest>> Update(UpdateContactRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
