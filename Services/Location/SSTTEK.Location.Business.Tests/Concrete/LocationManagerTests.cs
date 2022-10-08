using AutoFixture;
using EntityBase.Abstract;
using EntityBase.Concrete;
using EntityBase.Enum;
using EntityBase.Poco.Responses;
using Moq;
using ServerBaseContract.Repository.Abstract;
using SSTTEK.Location.AmqpService.Publisher.Location;
using SSTTEK.Location.Business.Concrete;
using SSTTEK.Location.Business.Contracts;
using SSTTEK.Location.DataAccess.Contract;
using SSTTEK.Location.Entities.Db;
using SSTTEK.Location.Entities.Poco.Location;
using SSTTEK.MassTransitCommon.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SSTTEK.Location.Business.Tests.Concrete
{
    public class LocationManagerTests : LocationTestBase
    {
        ITestOutputHelper _testOutputHelper;
        Mock<ILocationDal> _mockLocationDal;
        Mock<IQueryableRepositoryBase<LocationEntity>> _mockQueryable;
        ILocationService _locationService;
        Mock<ILocationPublisher> _mockLocationPublisher;
        public LocationManagerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _mockLocationPublisher = GenerateMock<ILocationPublisher>();
            _mockLocationDal = GenerateMock<ILocationDal>();
            _mockQueryable = GenerateMock<IQueryableRepositoryBase<LocationEntity>>();
            _locationService = new LocationManager(_mockLocationDal.Object, _mockQueryable.Object, _mockLocationPublisher.Object);
        }

        [Fact]
        public async Task Create_WhenSetStateFail_ReturnFailResponse()
        {
            //Arrange
            _mockLocationDal.Setup(w => w.SetState(It.IsAny<LocationEntity>(), OperationType.Create))
                .Returns((LocationEntity)null)
                .Verifiable();

            //Act
            var result = await _locationService.Create(Fixture.Create<CreateLocationRequest>());

            //Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("500", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Create_WhenSetStateSuccess_ReturnSuccessResponse()
        {
            //Arrange
            _mockLocationDal.Setup(w => w.SetState(It.IsAny<LocationEntity>(), OperationType.Create))
                .Returns(Fixture.Create<LocationEntity>())
                .Verifiable();


            //Act
            var result = await _locationService.Create(Fixture.Create<CreateLocationRequest>());

            //Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Delete_WhenSetStateFail_ReturnFailResponse()
        {
            //Arrange
            _mockQueryable.Setup(w => w.List<LocationResponse>(It.IsAny<FilterModel>(), null))
                .Returns(Fixture.Create<Task<IListModel<LocationResponse>>>())
                .Verifiable();
            _mockLocationDal.Setup(w => w.SetState(It.IsAny<IEnumerable<LocationEntity>>(), OperationType.Delete))
                .Returns((IEnumerable<LocationEntity>)null)
                .Verifiable();

            //Act
            var result = await _locationService.Delete(Fixture.Create<FilterModel>());

            //Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("500", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Delete_WhenSetStateSuccess_ReturnSuccessResponse()
        {
            //Arrange
            _mockQueryable.Setup(w => w.List<LocationResponse>(It.IsAny<FilterModel>(), null))
                .Returns(Fixture.Create<Task<IListModel<LocationResponse>>>())
                .Verifiable();
            _mockLocationDal.Setup(w => w.SetState(It.IsAny<IEnumerable<LocationEntity>>(), OperationType.Delete))
                .Returns(Fixture.Create<IEnumerable<LocationEntity>>())
                .Verifiable();

            //Act
            var result = await _locationService.Delete(Fixture.Create<FilterModel>());

            //Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("201", result.StatusCode.ToString());
        }

        [Fact]
        public async Task GetList_WhenListIsEmpty_ReturnFailResponse()
        {

            //Arrange
            var data = Fixture.Create<Task<IListModel<LocationResponse>>>();
            data.Result.Items = new List<LocationResponse>();
            _mockQueryable.Setup(w => w.List<LocationResponse>(It.IsAny<FilterModel>(), null))
                .Returns(data)
                .Verifiable();

            //Act
            var result = await _locationService.GetList(Fixture.Create<FilterModel>());

            //Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("404", result.StatusCode.ToString());
        }

        [Fact]
        public async Task GetList_WhenDataFound_ReturnSuccessResponse()
        {
            //Arrange
            _mockQueryable.Setup(w => w.List<LocationResponse>(It.IsAny<FilterModel>(), null))
                .Returns(Fixture.Create<Task<IListModel<LocationResponse>>>())
                .Verifiable();

            //Act
            var result = await _locationService.GetList(Fixture.Create<FilterModel>());

            //Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Get_WhenDataNotFound_ReturnFailResponse()
        {

            //Arrange
            _mockQueryable.Setup(w => w.FirstOrDefault<LocationResponse>(It.IsAny<FilterModel>(), null))
                .Returns(Task.FromResult<LocationResponse>(null)).Verifiable();

            //Act
            var result = await _locationService.Get(Fixture.Create<FilterModel>());

            //Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("404", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Get_WhenDataFound_ReturnSuccessResponse()
        {
            //Arrange
            _mockQueryable.Setup(w => w.FirstOrDefault<LocationResponse>(It.IsAny<FilterModel>(), null))
                .Returns(Fixture.Create<Task<LocationResponse>>())
                .Verifiable();

            //Act
            var result = await _locationService.Get(Fixture.Create<FilterModel>());

            //Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Update_WhenSetStateFail_ReturnFailResponse()
        {
            //Arrange
            _mockLocationDal.Setup(w => w.SetState(It.IsAny<LocationEntity>(), OperationType.Update))
                .Returns((LocationEntity)null)
                .Verifiable();

            //Act
            var result = await _locationService.Update(Fixture.Create<UpdateLocationRequest>());

            //Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("500", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Update_WhenSetStateSuccess_ReturnSuccessResponse()
        {
            //Arrange
            var getResponse = Fixture.Create<Task<Response<LocationResponse>>>();
            getResponse.Result.IsSuccessful = true;
            getResponse.Result.Data = new LocationResponse { Name = "Test" };
            _mockLocationDal.Setup(w => w.SetState(It.IsAny<LocationEntity>(), OperationType.Update))
                .Returns(Fixture.Create<LocationEntity>())
                .Verifiable();
            _mockLocationPublisher.Setup(w => w.PublishLocationModifiedEvent(It.IsAny<LocationModifiedEvent>()))
                .Verifiable();
            _mockQueryable.Setup(w => w.FirstOrDefault<LocationResponse>(It.IsAny<FilterModel>(), null))
                .Returns(Task.FromResult(Fixture.Create<LocationResponse>()))
                .Verifiable();

            //Act
            var result = await _locationService.Update(Fixture.Create<UpdateLocationRequest>());

            //Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("201", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Update_WhenDataNotFound_ReturnSuccessResponse()
        {
            //Arrange
            _mockLocationDal.Setup(w => w.SetState(It.IsAny<LocationEntity>(), OperationType.Update))
                .Returns(Fixture.Create<LocationEntity>())
                .Verifiable();
            _mockQueryable.Setup(w => w.FirstOrDefault<LocationResponse>(It.IsAny<FilterModel>(), null))
                .Returns(Task.FromResult<LocationResponse>(null))
                .Verifiable();

            //Act
            var result = await _locationService.Update(Fixture.Create<UpdateLocationRequest>());

            //Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("404", result.StatusCode.ToString());
        }

    }
}
