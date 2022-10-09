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
        ILocationService _locationService;
        Mock<ILocationPublisher> _mockLocationPublisher;
        public LocationManagerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _mockLocationPublisher = GenerateMock<ILocationPublisher>();
            _mockLocationDal = GenerateMock<ILocationDal>();
            _locationService = new LocationManager(_mockLocationDal.Object, _mockLocationPublisher.Object);
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
        public async Task GetList_WhenDataFound_ReturnSuccessResponse()
        {
            //Arrange
            _mockLocationDal.Setup(w => w.GetListAsync(null))
                .Returns(Fixture.Create<Task<List<LocationEntity>>>())
                .Verifiable();

            //Act
            var result = await _locationService.GetList();

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

       
    }
}
