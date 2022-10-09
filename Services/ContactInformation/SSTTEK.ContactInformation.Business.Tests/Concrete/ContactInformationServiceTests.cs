using AutoFixture;
using EntityBase.Abstract;
using EntityBase.Concrete;
using EntityBase.Enum;
using Moq;
using ServerBaseContract.Repository.Abstract;
using SSTTEK.ContactInformation.Business.Concrete;
using SSTTEK.ContactInformation.Business.Contracts;
using SSTTEK.ContactInformation.DataAccess.Contract;
using SSTTEK.ContactInformation.Entities.Db;
using SSTTEK.ContactInformation.Entities.Poco.ContactInformationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SSTTEK.ContactInformation.Business.Tests.Concrete
{
    public class ContactInformationServiceTests : ContactInformationTestBase
    {
        ITestOutputHelper _testOutputHelper;
        Mock<IContactInformationDal> _mockContactInformationDal;
        Mock<IQueryableRepositoryBase<ContactInformationEntity>> _mockQueryable;
        IContactInformationService _contactInformationService;
        public ContactInformationServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _mockContactInformationDal = GenerateMock<IContactInformationDal>();
            _mockQueryable = GenerateMock<IQueryableRepositoryBase<ContactInformationEntity>>();
            _contactInformationService = new ContactInformationManager(_mockContactInformationDal.Object, _mockQueryable.Object);
        }

        [Fact]
        public async Task Create_WhenSetStateFail_ReturnFailResponse()
        {
            //Arrange
            _mockContactInformationDal.Setup(w => w.SetState(It.IsAny<ContactInformationEntity>(), OperationType.Create))
                .Returns((ContactInformationEntity)null)
                .Verifiable();

            //Act
            var result = await _contactInformationService.Create(Fixture.Create<CreateContactInformationRequest>());

            //Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("500", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Create_WhenSetStateSuccess_ReturnSuccessResponse()
        {
            //Arrange
            _mockContactInformationDal.Setup(w => w.SetState(It.IsAny<ContactInformationEntity>(), OperationType.Create))
                .Returns(Fixture.Create<ContactInformationEntity>())
                .Verifiable();

            //Act
            var result = await _contactInformationService.Create(Fixture.Create<CreateContactInformationRequest>());

            //Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public async Task CreateList_WhenSetStateFail_ReturnFailResponse()
        {
            //Arrange
            _mockContactInformationDal.Setup(w => w.SetState(It.IsAny<List<ContactInformationEntity>>(), OperationType.Create))
                .Returns((List<ContactInformationEntity>)null)
                .Verifiable();

            //Act
            var result = await _contactInformationService.Create(Fixture.Create<List<CreateContactInformationRequest>>());

            //Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("500", result.StatusCode.ToString());
        }

        [Fact]
        public async Task CreateList_WhenSetStateSuccess_ReturnSuccessResponse()
        {
            //Arrange
            _mockContactInformationDal.Setup(w => w.SetState(It.IsAny<List<ContactInformationEntity>>(), OperationType.Create))
                .Returns(Fixture.Create<List<ContactInformationEntity>>())
                .Verifiable();

            //Act
            var result = await _contactInformationService.Create(Fixture.Create<List<CreateContactInformationRequest>>());

            //Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public async Task GetList_WhenListIsEmpty_ReturnFailResponse()
        {

            //Arrange
            var data = Fixture.Create<Task<IListModel<ContactInformationResponse>>>();
            data.Result.Items = new List<ContactInformationResponse>();
            _mockQueryable.Setup(w => w.List<ContactInformationResponse>(It.IsAny<FilterModel>(), null))
                .Returns(data)
                .Verifiable();

            //Act
            var result = await _contactInformationService.GetList(Fixture.Create<FilterModel>());

            //Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("404", result.StatusCode.ToString());
        }

        [Fact]
        public async Task GetList_WhenDataFound_ReturnSuccessResponse()
        {
            //Arrange
            _mockQueryable.Setup(w => w.List<ContactInformationResponse>(It.IsAny<FilterModel>(), null))
                .Returns(Fixture.Create<Task<IListModel<ContactInformationResponse>>>())
                .Verifiable();

            //Act
            var result = await _contactInformationService.GetList(Fixture.Create<FilterModel>());

            //Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Get_WhenDataNotFound_ReturnFailResponse()
        {

            //Arrange
            _mockQueryable.Setup(w => w.FirstOrDefault<ContactInformationResponse>(It.IsAny<FilterModel>(), null))
                .Returns(Task.FromResult<ContactInformationResponse>(null)).Verifiable();

            //Act
            var result = await _contactInformationService.Get(Fixture.Create<FilterModel>());

            //Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("404", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Get_WhenDataFound_ReturnSuccessResponse()
        {
            //Arrange
            _mockQueryable.Setup(w => w.FirstOrDefault<ContactInformationResponse>(It.IsAny<FilterModel>(), null))
                .Returns(Fixture.Create<Task<ContactInformationResponse>>())
                .Verifiable();

            //Act
            var result = await _contactInformationService.Get(Fixture.Create<FilterModel>());

            //Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Remove_WhenSetStateFail_ReturnFailResponse()
        {
            //Arrange
            _mockContactInformationDal.Setup(w => w.SetState(It.IsAny<IEnumerable<ContactInformationEntity>>(), OperationType.Remove))
                .Returns((IEnumerable<ContactInformationEntity>)null)
                .Verifiable();

            //Act
            var result = await _contactInformationService.Remove(Fixture.Create<FilterModel>());

            //Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("500", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Remove_WhenSetStateSuccess_ReturnSuccessResponse()
        {
            //Arrange
            _mockContactInformationDal.Setup(w => w.SetState(It.IsAny<ContactInformationEntity>(), OperationType.Remove))
                .Returns(Fixture.Create<ContactInformationEntity>())
                .Verifiable();

            //Act
            var result = await _contactInformationService.Remove(Fixture.Create<FilterModel>());

            //Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("201", result.StatusCode.ToString());
        }
    }
}
