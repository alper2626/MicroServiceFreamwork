using AutoMapperAdapter;
using EntityBase.Concrete;
using EntityBase.Enum;
using EntityBase.Poco.Responses;
using RestHelpers.Constacts;
using ServerBaseContract.Repository.Abstract;
using SSTTEK.Contact.AmqpService.Sender.ContactInformation;
using SSTTEK.Contact.Business.Contract;
using SSTTEK.Contact.Business.HttpClients;
using SSTTEK.Contact.DataAccess.Contract;
using SSTTEK.Contact.Entities.Db;
using SSTTEK.Contact.Entities.Poco.ContactDto;
using SSTTEK.Contact.Entities.Poco.ContactInformationDto;
using SSTTEK.MassTransitCommon.Commands;

namespace SSTTEK.Contact.Business.Concrete
{
    public class ContactManager : IContactService
    {
        IContactDal _contactDal;
        IQueryableRepositoryBase<ContactEntity> _queryableRepositoryBase;
        IContactInformationClient _contactInformationClient;
        IContactInformationSender _contactInformationSender;

        public ContactManager(IContactDal contactDal, IQueryableRepositoryBase<ContactEntity> queryableRepositoryBase, IContactInformationClient contactInformationClient, IContactInformationSender contactInformationSender)
        {
            _contactDal = contactDal;
            _queryableRepositoryBase = queryableRepositoryBase;
            _contactInformationClient = contactInformationClient;
            _contactInformationSender = contactInformationSender;
        }

        public async Task<Response<CreateContactRequest>> Create(CreateContactRequest request)
        {
            var res = _contactDal.SetState(AutoMapperWrapper.Mapper.Map<CreateContactRequest, ContactEntity>(request), OperationType.Create);
            if (res == null)
            {
                return Response<CreateContactRequest>.Fail(CommonMessage.ServerError, 500);
            }
            var command = new CreateContactInformationCommandWrapper
            {
                Items = AutoMapperWrapper.Mapper.Map<List<CreateContactInformationCommand>>(request.ContactInformations)
            };
            command.Items.ForEach(item =>
            {
                item.EventCreatedTime = DateTime.Now;
                item.EventOwner = "ContactManager --> Create";
            });
            await _contactInformationSender.PublishContactInformations(command);
            return Response<CreateContactRequest>.Success(CommonMessage.Success, 200);
        }

        public async Task<Response<IEnumerable<ContactResponse>>> Delete(FilterModel request)
        {
            var res = await _queryableRepositoryBase.List<ContactResponse>(request,null);
            var entities = AutoMapperWrapper.Mapper.Map<List<ContactEntity>>(res.Items);
            if (_contactDal.SetState(entities, OperationType.Delete) == null)
            {
                return Response<IEnumerable<ContactResponse>>.Fail(CommonMessage.ServerError, 500);
            }
            return Response<IEnumerable<ContactResponse>>.Success(res.Items, 201);
        }

        public async Task<Response<IEnumerable<ContactResponse>>> GetList(FilterModel request)
        {
            var res = await _queryableRepositoryBase.List<ContactResponse>(request);

            if (!res.Items.Any())
            {
                return Response<IEnumerable<ContactResponse>>.Fail(CommonMessage.NotFound, 404);
            }
            return Response<IEnumerable<ContactResponse>>.Success(res.Items, 200);
        }

        public async Task<Response<ContactResponse>> Get(FilterModel request)
        {
            var res = await _queryableRepositoryBase.FirstOrDefault<ContactResponse>(request);

            if (res == null)
            {
                return Response<ContactResponse>.Fail(CommonMessage.NotFound, 404);
            }
            return Response<ContactResponse>.Success(res, 200);
        }

        public async Task<Response<ContactDetailedResponse>> GetWithDetail(FilterModel request)
        {
            var res = await _queryableRepositoryBase.FirstOrDefault<ContactDetailedResponse>(request);

            if (res == null)
            {
                return Response<ContactDetailedResponse>.Fail(CommonMessage.NotFound, 404);
            }

            var httpRes = await _contactInformationClient.ListContactInformationAsync(FilterModel.Get(nameof(ContactInformationResponse.ContactEntityId), FilterOperator.Equals, res.Id));
            if (!httpRes.IsSuccessful)
            {
                return httpRes.FailConvert<ContactDetailedResponse>();
            }

            res.Informations = httpRes.Data;
            return Response<ContactDetailedResponse>.Success(res, 200);

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
