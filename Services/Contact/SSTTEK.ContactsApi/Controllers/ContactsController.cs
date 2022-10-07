using EntityBase.Concrete;
using EntityBase.Poco.Responses;
using Microsoft.AspNetCore.Mvc;
using RestHelpers.Controllers;
using SSTTEK.Contact.Entities.Poco.ContactDto;

namespace SSTTEK.Contact.Api.Controllers
{
    [Route("api/[controller]")]
    public class ContactsController : ApiControllerBase
    {
        [HttpPost("create")]
        public IActionResult Create(CreateContactRequest request)
        {
            return CreateActionResult(Response<CreateContactRequest>.Fail("Metot Not Implemented", 400));
        }

        [HttpPut("update")]
        public IActionResult Update(UpdateContactRequest request)
        {
            return CreateActionResult(Response<UpdateContactRequest>.Fail("Metot Not Implemented", 400));
        }

        [HttpPut("remove")]
        public IActionResult Remove(FilterModel model)
        {
            return CreateActionResult(Response<FilterModel>.Fail("Metot Not Implemented", 400));
        }

        [HttpGet("get")]
        public IActionResult Get(FilterModel request)
        {
            return CreateActionResult(Response<FilterModel>.Fail("Metot Not Implemented", 400));
        }

        [HttpDelete("delete")]
        public IActionResult Delete(FilterModel request)
        {
            return CreateActionResult(Response<FilterModel>.Fail("Metot Not Implemented", 400));
        }
    }
}
