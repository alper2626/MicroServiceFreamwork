using AutoMapperAdapter;
using EntityBase.Concrete;
using EntityBase.Enum;
using EntityBase.Poco.Responses;
using RestHelpers.Constacts;
using ServerBaseContract.Repository.Abstract;
using SSTTEK.ContactInformation.Business.Contracts;
using SSTTEK.ContactInformation.DataAccess.Contract;
using SSTTEK.ContactInformation.Entities.Db;
using SSTTEK.ContactInformation.Entities.Poco.ContactInformationDto;

namespace SSTTEK.ContactInformation.Business.Concrete
{
    public class ContactInformationManager : IContactInformationService
    {
        IContactInformationDal _contactInformationDal;
        IQueryableRepositoryBase<ContactInformationEntity> _queryableRepositoryBase;
        public ContactInformationManager(IContactInformationDal contactInformationDal, IQueryableRepositoryBase<ContactInformationEntity> queryableRepositoryBase)
        {
            _contactInformationDal = contactInformationDal;
            _queryableRepositoryBase = queryableRepositoryBase;
        }

        public async Task<Response<CreateContactInformationRequest>> Create(CreateContactInformationRequest request)
        {
            var res = _contactInformationDal.SetState(AutoMapperWrapper.Mapper.Map<CreateContactInformationRequest, ContactInformationEntity>(request), OperationType.Create);
            if (res == null)
            {
                return Response<CreateContactInformationRequest>.Fail(CommonMessage.ServerError, 500);
            }
            return Response<CreateContactInformationRequest>.Success(CommonMessage.Success, 200);
        }

        public async Task<Response<IEnumerable<ContactInformationResponse>>> GetList(FilterModel request)
        {
            var res = await _queryableRepositoryBase.List<ContactInformationResponse>(request);

            if (!res.Items.Any())
            {
                return Response<IEnumerable<ContactInformationResponse>>.Fail(CommonMessage.NotFound, 404);
            }
            return Response<IEnumerable<ContactInformationResponse>>.Success(res.Items, 200);
        }

        public async Task<Response<ContactInformationResponse>> Get(FilterModel request)
        {
            var res = await _queryableRepositoryBase.FirstOrDefault<ContactInformationResponse>(request);

            if (res == null)
            {
                return Response<ContactInformationResponse>.Fail(CommonMessage.NotFound, 404);
            }
            return Response<ContactInformationResponse>.Success(res, 200);
        }


        public async Task<Response<IEnumerable<ContactInformationResponse>>> Remove(FilterModel request)
        {
            var res = await _queryableRepositoryBase.List<ContactInformationResponse>(request);
            var entities = AutoMapperWrapper.Mapper.Map<List<ContactInformationEntity>>(res.Items);
            if (_contactInformationDal.SetState(entities, OperationType.Remove) == null)
            {
                return Response<IEnumerable<ContactInformationResponse>>.Fail(CommonMessage.ServerError, 500);
            }
            return Response<IEnumerable<ContactInformationResponse>>.Success(res.Items, 201);
        }

    }
}
