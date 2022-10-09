using AutoMapperAdapter;
using EntityBase.Enum;
using EntityBase.Poco.Responses;
using RestHelpers.Constacts;
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
        ILocationPublisher _locationPublisher;

        public LocationManager(ILocationDal locationDal, ILocationPublisher locationPublisher)
        {
            _locationDal = locationDal;
            _locationPublisher = locationPublisher;
        }

        public async Task<Response<CreateLocationRequest>> Create(CreateLocationRequest request)
        {
            var res = _locationDal.SetState(AutoMapperWrapper.Mapper.Map<CreateLocationRequest, LocationEntity>(request), OperationType.Create);
            if (res == null)
            {
                return Response<CreateLocationRequest>.Fail(CommonMessage.ServerError, 500);
            }
            return Response<CreateLocationRequest>.Success(request, 200);
        }

        public async Task<Response<LocationResponse>> Delete(Guid id)
        {
            
            var res = await _locationDal.GetAsync(w => w.Id == id);

            if (res == null)
            {
                return Response<LocationResponse>.Fail(CommonMessage.NotFound, 404);
            }

            if (_locationDal.SetState(res, OperationType.Delete) == null)
            {
                return Response<LocationResponse>.Fail(CommonMessage.ServerError, 500);
            }
            return Response<LocationResponse>.Success(AutoMapperWrapper.Mapper.Map<LocationResponse>(res), 201);
        }

        public async Task<Response<IEnumerable<LocationResponse>>> GetList()
        {
            return Response<IEnumerable<LocationResponse>>.Success(AutoMapperWrapper.Mapper.Map<List<LocationResponse>>(await _locationDal.GetListAsync()), 200);
        }


        public async Task<Response<UpdateLocationRequest>> Update(UpdateLocationRequest request)
        {
            //Not : ContactInformation da mail telefon konum gibi farklı bilgiler tutulduğu için LocationId bilgisini yazamadım.
            //Bu yüzden ContactInformation Modulde Content alanına index attım eski ismi ile sorgulatıp update edeceğim.
            var data = await _locationDal.GetAsync(w => w.Id == request.Id);
            if (data == null)
            {
                return Response<UpdateLocationRequest>.Fail(CommonMessage.NotFound, 404);
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
                OldName = data.Name.ToLower()
            };

            await _locationPublisher.PublishLocationModifiedEvent(@event);

            return Response<UpdateLocationRequest>.Success(request, 201);
        }
    }
}
