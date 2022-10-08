using AutoMapperAdapter;
using EntityBase.Concrete;
using EntityBase.Enum;
using EntityBase.Poco.Responses;
using RestHelpers.Constacts;
using ServerBaseContract.Repository.Abstract;
using SSTTEK.Location.Business.Contracts;
using SSTTEK.Location.DataAccess.Contract;
using SSTTEK.Location.Entities.Db;
using SSTTEK.Location.Entities.Poco.Location;

namespace SSTTEK.Location.Business.Concrete
{
    public class LocationManager : ILocationService
    {
        ILocationDal _locationDal;
        IQueryableRepositoryBase<LocationEntity> _queryableRepositoryBase;

        public LocationManager(ILocationDal locationDal, IQueryableRepositoryBase<LocationEntity> queryableRepositoryBase)
        {
            _locationDal = locationDal;
            _queryableRepositoryBase = queryableRepositoryBase;
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

            if (res == null || !res.Items.Any())
            {
                return Response<IEnumerable<LocationResponse>>.Fail(CommonMessage.NotFound, 404);
            }
            return Response<IEnumerable<LocationResponse>>.Success(res.Items, 201);
        }

        public async Task<Response<LocationResponse>> Get(FilterModel request)
        {
            var res = await _queryableRepositoryBase.FirstOrDefault<LocationResponse>(request);

            if (res == null)
            {
                return Response<LocationResponse>.Fail(CommonMessage.NotFound, 404);
            }
            return Response<LocationResponse>.Success(res, 201);
        }

        public async Task<Response<UpdateLocationRequest>> Update(UpdateLocationRequest request)
        {
            var res = _locationDal.SetState(AutoMapperWrapper.Mapper.Map<UpdateLocationRequest, LocationEntity>(request), OperationType.Update);
            if (res == null)
            {
                return Response<UpdateLocationRequest>.Fail(CommonMessage.ServerError, 500);
            }
            return Response<UpdateLocationRequest>.Success(CommonMessage.Success, 201);
        }
    }
}
