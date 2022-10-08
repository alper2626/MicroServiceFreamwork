using EntityBase.Concrete;
using EntityBase.Poco.Responses;
using Microsoft.AspNetCore.Mvc;
using RestHelpers.Controllers;
using SSTTEK.Contact.Business.Contract;
using SSTTEK.Contact.Entities.Poco.ContactDto;

namespace SSTTEK.Contact.Api.Controllers
{
    [Route("api/[controller]")]
    public class ContactsController : ApiControllerBase
    {
        IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateContactRequest request)
        {
            return CreateActionResult(await _contactService.Create(request));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateContactRequest request)
        {
            return CreateActionResult(await _contactService.Update(request));
        }

        [HttpPut("remove")]
        public async Task<IActionResult> Remove(FilterModel request)
        {
            return CreateActionResult(await _contactService.Remove(request));
        }

        [HttpPost("get")]
        public async Task<IActionResult> Get(FilterModel request)
        {
            return CreateActionResult(await _contactService.Get(request));
        }

        [HttpPost("list")]
        public async Task<IActionResult> List(FilterModel request)
        {
            return CreateActionResult(await _contactService.GetList(request));
        }

        [HttpPost("getwithdetail")]
        public async Task<IActionResult> GetWithDetail(FilterModel request)
        {
            return CreateActionResult(await _contactService.GetWithDetail(request));
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(FilterModel request)
        {
            return CreateActionResult(await _contactService.Delete(request));
        }
    }
}
