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
using SSTTEK.MassTransitCommon.Events;

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
            return Response<CreateContactInformationRequest>.Success(request, 200);
        }

        public async Task<Response<List<CreateContactInformationRequest>>> Create(List<CreateContactInformationRequest> request)
        {
            var res = _contactInformationDal.SetState(AutoMapperWrapper.Mapper.Map<List<ContactInformationEntity>>(request), OperationType.Create);
            if (res == null)
            {
                return Response<List<CreateContactInformationRequest>>.Fail(CommonMessage.ServerError, 500);
            }
            return Response<List<CreateContactInformationRequest>>.Success(request, 200);
        }

        public async Task<Response<List<ContactInformationResponse>>> GetList(FilterModel request)
        {
            var res = await _queryableRepositoryBase.List<ContactInformationResponse>(request);

            if (!res.Items.Any())
            {
                return Response<List<ContactInformationResponse>>.Fail(CommonMessage.NotFound, 404);
            }
            return Response<List<ContactInformationResponse>>.Success(res.Items.ToList(), 200);
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


        public async Task<Response<List<ContactInformationResponse>>> Remove(FilterModel request)
        {
            var res = await _queryableRepositoryBase.List<ContactInformationResponse>(request);
            var entities = AutoMapperWrapper.Mapper.Map<List<ContactInformationEntity>>(res.Items);
            if (_contactInformationDal.SetState(entities, OperationType.Remove) == null)
            {
                return Response<List<ContactInformationResponse>>.Fail(CommonMessage.ServerError, 500);
            }
            return Response<List<ContactInformationResponse>>.Success(res.Items.ToList(), 201);
        }

        public async Task UpdateLocationNamesEventConsume(LocationModifiedEvent @event)
        {
            var resultSet = await _contactInformationDal.GetListAsync(w => w.ContentIndex == @event.OldName);
            resultSet.ForEach(x =>
            {
                x.ContentIndex = @event.NewName.ToLower();
                x.Content = @event.NewName;
            });
            _contactInformationDal.SetState(resultSet, OperationType.Update);
        }

    }
}
