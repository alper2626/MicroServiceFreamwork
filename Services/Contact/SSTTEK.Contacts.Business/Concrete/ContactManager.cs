using AutoMapperAdapter;
using EntityBase.Concrete;
using EntityBase.Enum;
using EntityBase.Poco.Responses;
using RestHelpers.Constacts;
using ServerBaseContract.Repository.Abstract;
using SSTTEK.Contact.Business.Contract;
using SSTTEK.Contact.DataAccess.Contract;
using SSTTEK.Contact.Entities.Db;
using SSTTEK.Contact.Entities.Poco.ContactDto;

namespace SSTTEK.Contact.Business.Concrete
{
    public class ContactManager : IContactService
    {
        IContactDal _contactDal;
        IQueryableRepositoryBase<ContactEntity> _queryableRepositoryBase;

        public ContactManager(IContactDal contactDal, IQueryableRepositoryBase<ContactEntity> queryableRepositoryBase)
        {
            _contactDal = contactDal;
            _queryableRepositoryBase = queryableRepositoryBase;
        }

        public async Task<Response<CreateContactRequest>> Create(CreateContactRequest request)
        {
            var res = _contactDal.SetState(AutoMapperWrapper.Mapper.Map<CreateContactRequest, ContactEntity>(request), OperationType.Create);
            if(res == null)
            {
                return Response<CreateContactRequest>.Fail(CommonMessage.ServerError, 500);
            }
            return Response<CreateContactRequest>.Success(CommonMessage.Success, 200);
        }

        public async Task<Response<IEnumerable<ContactResponse>>> Delete(FilterModel request)
        {
            var res = await _queryableRepositoryBase.List<ContactResponse>(request);
            var entities = AutoMapperWrapper.Mapper.Map<List<ContactEntity>>(res.Items);
            if(_contactDal.SetState(entities, OperationType.Delete) == null)
            {
                return Response<IEnumerable<ContactResponse>>.Fail(CommonMessage.ServerError, 500);
            }
            return Response<IEnumerable<ContactResponse>>.Success(res.Items, 201);
        }

        public async Task<Response<IEnumerable<ContactResponse>>> GetList(FilterModel request)
        {
            var res = await _queryableRepositoryBase.List<ContactResponse>(request);
            
            if (res == null || !res.Items.Any())
            {
                return Response<IEnumerable<ContactResponse>>.Fail(CommonMessage.NotFound, 404);
            }
            return Response<IEnumerable<ContactResponse>>.Success(res.Items, 201);
        }

        public async Task<Response<ContactResponse>> Get(FilterModel request)
        {
            var res = await _queryableRepositoryBase.FirstOrDefault<ContactResponse>(request);

            if (res == null)
            {
                return Response<ContactResponse>.Fail(CommonMessage.NotFound, 404);
            }
            return Response<ContactResponse>.Success(res, 201);
        }

        public async Task<Response<IEnumerable<ContactDetailedResponse>>> GetWithDetail(FilterModel request)
        {
            //TODO: Alper Api Request Atılacak sona bırak.
            throw new NotImplementedException();
        }

        public async Task<Response<IEnumerable<ContactResponse>>> Remove(FilterModel request)
        {
            var res = await _queryableRepositoryBase.List<ContactResponse>(request);
            var entities = AutoMapperWrapper.Mapper.Map<List<ContactEntity>>(res.Items);
            if (_contactDal.SetState(entities, OperationType.Remove) == null)
            {
                return Response<IEnumerable<ContactResponse>>.Fail(CommonMessage.ServerError, 500);
            }
            return Response<IEnumerable<ContactResponse>>.Success(res.Items, 201);
        }

        public async Task<Response<UpdateContactRequest>> Update(UpdateContactRequest request)
        {
            var res = _contactDal.SetState(AutoMapperWrapper.Mapper.Map<UpdateContactRequest, ContactEntity>(request), OperationType.Update);
            if (res == null)
            {
                return Response<UpdateContactRequest>.Fail(CommonMessage.ServerError, 500);
            }
            return Response<UpdateContactRequest>.Success(CommonMessage.Success, 201);
        }
    }
}
