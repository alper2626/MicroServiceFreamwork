using AutoFixture;
using AutoMapper;
using EntityBase.Abstract;
using EntityBase.Concrete;
using EntityBase.Enum;
using EntityBase.Poco.Responses;
using Moq;
using ServerBaseContract.Repository.Abstract;
using SSTTEK.Contact.AmqpService.Sender.ContactInformation;
using SSTTEK.Contact.Business.Concrete;
using SSTTEK.Contact.Business.Contract;
using SSTTEK.Contact.Business.HttpClients;
using SSTTEK.Contact.DataAccess.Contract;
using SSTTEK.Contact.Entities.Db;
using SSTTEK.Contact.Entities.Poco.ContactDto;
using SSTTEK.Contact.Entities.Poco.ContactInformationDto;
using SSTTEK.MassTransitCommon.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SSTTEK.Contact.Business.Tests.Concrete
{
    public class ContactServiceTests : ContactTestBase
    {
        ITestOutputHelper _testOutputHelper;
        Mock<IContactDal> _mockContactDal;
        Mock<IQueryableRepositoryBase<ContactEntity>> _mockQueryable;
        Mock<IContactInformationClient> _mockContactInformationClient;
        Mock<IContactInformationSender> _mockContactInformationSender;
        IContactService _contactService;
        public ContactServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _mockContactDal = GenerateMock<IContactDal>();
            _mockQueryable = GenerateMock<IQueryableRepositoryBase<ContactEntity>>();
            _mockContactInformationClient = GenerateMock<IContactInformationClient>();
            _mockContactInformationSender = GenerateMock<IContactInformationSender>();
            _contactService = new ContactManager(_mockContactDal.Object, _mockQueryable.Object, _mockContactInformationClient.Object, _mockContactInformationSender.Object);
        }

        [Fact]
        public async Task Create_WhenSetStateFail_ReturnFailResponse()
        {
            //Arrange
            _mockContactDal.Setup(w => w.SetState(It.IsAny<ContactEntity>(), OperationType.Create))
                .Returns((ContactEntity)null)
                .Verifiable();

            //Act
            var result = await _contactService.Create(Fixture.Create<CreateContactRequest>());

            //Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("500", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Create_WhenSetStateSuccess_ReturnSuccessResponse()
        {
            //Arrange
            _mockContactDal.Setup(w => w.SetState(It.IsAny<ContactEntity>(), OperationType.Create))
                .Returns(Fixture.Create<ContactEntity>())
                .Verifiable();
            _mockContactInformationSender.Setup(w => w.PublishContactInformations(It.IsAny<CreateContactInformationCommandWrapper>()))
                .Verifiable();

            //Act
            var result = await _contactService.Create(Fixture.Create<CreateContactRequest>());

            //Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Delete_WhenSetStateFail_ReturnFailResponse()
        {
            //Arrange
            _mockQueryable.Setup(w => w.List<ContactResponse>(It.IsAny<FilterModel>(), null))
                .Returns(Fixture.Create<Task<IListModel<ContactResponse>>>())
                .Verifiable();
            _mockContactDal.Setup(w => w.SetState(It.IsAny<IEnumerable<ContactEntity>>(), OperationType.Delete))
                .Returns((IEnumerable<ContactEntity>)null)
                .Verifiable();

            //Act
            var result = await _contactService.Delete(Fixture.Create<FilterModel>());

            //Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("500", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Delete_WhenSetStateSuccess_ReturnSuccessResponse()
        {
            //Arrange
            _mockQueryable.Setup(w => w.List<ContactResponse>(It.IsAny<FilterModel>(), null))
                .Returns(Fixture.Create<Task<IListModel<ContactResponse>>>())
                .Verifiable();
            _mockContactDal.Setup(w => w.SetState(It.IsAny<IEnumerable<ContactEntity>>(), OperationType.Delete))
                .Returns(Fixture.Create<IEnumerable<ContactEntity>>())
                .Verifiable();

            //Act
            var result = await _contactService.Delete(Fixture.Create<FilterModel>());

            //Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("201", result.StatusCode.ToString());
        }

        [Fact]
        public async Task GetList_WhenListIsEmpty_ReturnFailResponse()
        {

            //Arrange
            var data = Fixture.Create<Task<IListModel<ContactResponse>>>();
            data.Result.Items = new List<ContactResponse>();
            _mockQueryable.Setup(w => w.List<ContactResponse>(It.IsAny<FilterModel>(), null))
                .Returns(data)
                .Verifiable();

            //Act
            var result = await _contactService.GetList(Fixture.Create<FilterModel>());

            //Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("404", result.StatusCode.ToString());
        }

        [Fact]
        public async Task GetList_WhenDataFound_ReturnSuccessResponse()
        {
            //Arrange
            _mockQueryable.Setup(w => w.List<ContactResponse>(It.IsAny<FilterModel>(), null))
                .Returns(Fixture.Create<Task<IListModel<ContactResponse>>>())
                .Verifiable();

            //Act
            var result = await _contactService.GetList(Fixture.Create<FilterModel>());

            //Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Get_WhenDataNotFound_ReturnFailResponse()
        {

            //Arrange
            _mockQueryable.Setup(w => w.FirstOrDefault<ContactResponse>(It.IsAny<FilterModel>(), null))
                .Returns(Task.FromResult<ContactResponse>(null)).Verifiable();

            //Act
            var result = await _contactService.Get(Fixture.Create<FilterModel>());

            //Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("404", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Get_WhenDataFound_ReturnSuccessResponse()
        {
            //Arrange
            _mockQueryable.Setup(w => w.FirstOrDefault<ContactResponse>(It.IsAny<FilterModel>(), null))
                .Returns(Fixture.Create<Task<ContactResponse>>())
                .Verifiable();

            //Act
            var result = await _contactService.Get(Fixture.Create<FilterModel>());

            //Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public async Task GetWithDetail_WhenDataNotFound_ReturnFailResponse()
        {

            //Arrange
            _mockQueryable.Setup(w => w.FirstOrDefault<ContactDetailedResponse>(It.IsAny<FilterModel>(), null))
                .Returns((Task<ContactDetailedResponse>)null).Verifiable();

            //Act
            var result = await _contactService.GetWithDetail(Fixture.Create<FilterModel>());

            //Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("404", result.StatusCode.ToString());
        }

        [Fact]
        public async Task GetWithDetail_WhenContentInformationResponseFail_ReturnFailResponse()
        {
            //Arrange
            var clientResponse = Response<List<ContactInformationResponse>>.Fail("", 404);
            _mockQueryable.Setup(w => w.FirstOrDefault<ContactDetailedResponse>(It.IsAny<FilterModel>(), null))
                .Returns(Fixture.Create<Task<ContactDetailedResponse>>())
                .Verifiable();
            _mockContactInformationClient.Setup(w => w.ListContactInformationAsync(It.IsAny<FilterModel>()))
                .Returns(Task.FromResult(clientResponse))
                .Verifiable();

            //Act
            var result = await _contactService.GetWithDetail(Fixture.Create<FilterModel>());

            //Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("404", result.StatusCode.ToString());
        }

        [Fact]
        public async Task GetWithDetail_WhenContentInformationResponseSuccessAndDataFound_ReturnSuccessResponse()
        {
            //Arrange
            _mockQueryable.Setup(w => w.FirstOrDefault<ContactResponse>(It.IsAny<FilterModel>(), null))
                .Returns(Fixture.Create<Task<ContactResponse>>())
                .Verifiable();
            _mockContactInformationClient.Setup(w => w.ListContactInformationAsync(It.IsAny<FilterModel>()))
                .Returns(Fixture.Create<Task<Response<List<ContactInformationResponse>>>>())
                .Verifiable();

            //Act
            var result = await _contactService.GetWithDetail(Fixture.Create<FilterModel>());

            //Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Update_WhenSetStateFail_ReturnFailResponse()
        {
            //Arrange
            _mockContactDal.Setup(w => w.SetState(It.IsAny<ContactEntity>(), OperationType.Update))
                .Returns((ContactEntity)null)
                .Verifiable();

            //Act
            var result = await _contactService.Update(Fixture.Create<UpdateContactRequest>());

            //Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("500", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Update_WhenSetStateSuccess_ReturnSuccessResponse()
        {
            //Arrange
            _mockContactDal.Setup(w => w.SetState(It.IsAny<ContactEntity>(), OperationType.Update))
                .Returns(Fixture.Create<ContactEntity>())
                .Verifiable();

            //Act
            var result = await _contactService.Update(Fixture.Create<UpdateContactRequest>());

            //Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("201", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Remove_WhenSetStateFail_ReturnFailResponse()
        {
            //Arrange
            _mockContactDal.Setup(w => w.SetState(It.IsAny<IEnumerable<ContactEntity>>(), OperationType.Remove))
                .Returns((IEnumerable<ContactEntity>)null)
                .Verifiable();

            //Act
            var result = await _contactService.Remove(Fixture.Create<FilterModel>());

            //Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("500", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Remove_WhenSetStateSuccess_ReturnSuccessResponse()
        {
            //Arrange
            _mockContactDal.Setup(w => w.SetState(It.IsAny<ContactEntity>(), OperationType.Remove))
                .Returns(Fixture.Create<ContactEntity>())
                .Verifiable();

            //Act
            var result = await _contactService.Remove(Fixture.Create<FilterModel>());

            //Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("201", result.StatusCode.ToString());
        }


    }
}
