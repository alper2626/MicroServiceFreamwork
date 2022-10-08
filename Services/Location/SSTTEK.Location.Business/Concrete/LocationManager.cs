using AutoMapperAdapter; 
using EntityBase.Concrete;
using EntityBase.Enum;
using EntityBase.Poco.Responses;
using RestHelpers.Constacts;
using ServerBaseContract.Repository.Abstract;
using SSTTEK.Location.AmqpService.Publisher.Location;
using SSTTEK.Location.Business.Contracts;
using SSTTEK.Location.DataAccess.Contract;
using SSTTEK.Location.Entities.Db;
using SSTTEK.Location.Entities.Poco.Location;
using SSTTEK.MassTransitCommon.Events;

namespace SSTTEK.Location.Business.Concrete
{
    public class LocationManager : ILocationService
    {
        ILocationDal _locationDal;
        IQueryableRepositoryBase<LocationEntity> _queryableRepositoryBase;
        ILocationPublisher _locationPublisher;

        public LocationManager(ILocationDal locationDal, IQueryableRepositoryBase<LocationEntity> queryableRepositoryBase, ILocationPublisher locationPublisher)
        {
            _locationDal = locationDal;
            _queryableRepositoryBase = queryableRepositoryBase;
            _locationPublisher = locationPublisher;
        }

        public async Task<Response<CreateLocationRequest>> Create(CreateLocationRequest request)
        {
            var res = _locationDal.SetState(AutoMapperWrapper.Mapper.Map<CreateLocationRequest, LocationEntity>(request), OperationType.Create);
            if (res == null)
            {
                return Response<CreateLocationRequest>.Fail(CommonMessage.ServerError, 500);
            }
            return Response<CreateLocationRequest>.Success(CommonMessage.Success, 200);
        }

        public async Task<Response<IEnumerable<LocationResponse>>> Delete(FilterModel request)
        {
            var res = await _queryableRepositoryBase.List<LocationResponse>(request);
            var entities = AutoMapperWrapper.Mapper.Map<List<LocationEntity>>(res.Items);
            if (_locationDal.SetState(entities, OperationType.Delete) == null)
            {
                return Response<IEnumerable<LocationResponse>>.Fail(CommonMessage.ServerError, 500);
            }
            return Response<IEnumerable<LocationResponse>>.Success(res.Items, 201);
        }

        public async Task<Response<IEnumerable<LocationResponse>>> GetList(FilterModel request)
        {
            var res = await _queryableRepositoryBase.List<LocationResponse>(request);

            if (!res.Items.Any())
            {
                return Response<IEnumerable<LocationResponse>>.Fail(CommonMessage.NotFound, 404);
            }
            return Response<IEnumerable<LocationResponse>>.Success(res.Items, 200);
        }

        public async Task<Response<LocationResponse>> Get(FilterModel request)
        {
            var res = await _queryableRepositoryBase.FirstOrDefault<LocationResponse>(request);

            if (res == null)
            {
                return Response<LocationResponse>.Fail(CommonMessage.NotFound, 404);
            }
            return Response<LocationResponse>.Success(res, 200);
        }

        public async Task<Response<UpdateLocationRequest>> Update(UpdateLocationRequest request)
        {
            //Not : ContactInformation da mail telefon konum gibi farklı bilgiler tutulduğu için LocationId bilgisini yazamadım.
            //Bu yüzden ContactInformation Modulde Content alanına index attım eski ismi ile sorgulatıp update edeceğim.
            var data = await this.Get(FilterModel.Get(nameof(LocationEntity.Id), FilterOperator.Equals, request.Id));
            if (!data.IsSuccessful)
            {
                return data.FailConvert<UpdateLocationRequest>();
            }

            var res = _locationDal.SetState(AutoMapperWrapper.Mapper.Map<UpdateLocationRequest, LocationEntity>(request), OperationType.Update);
            if (res == null)
            {
                return Response<UpdateLocationRequest>.Fail(CommonMessage.ServerError, 500);
            }

            var @event = new LocationModifiedEvent
            {
                EventOwner = "Location Manager -->  Update",
                EventCreatedTime = DateTime.Now,
                NewName = request.Name,
                OldName = data.Data.Name.ToLower()
            };

            await _locationPublisher.PublishLocationModifiedEvent(@event);

            return Response<UpdateLocationRequest>.Success(CommonMessage.Success, 201);
        }
    }
}
