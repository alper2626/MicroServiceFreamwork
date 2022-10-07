using EntityBase.Poco.Responses;
using Microsoft.AspNetCore.Mvc;
using RestHelpers.Controllers;

namespace SSTTEK.ContactsApi.Controllers
{
    public class ContactInformationController : ApiControllerBase
    {
        public IActionResult Index()
        {
            return CreateActionResult(Response<NoContent>.Fail("test",400));

        }
    }
}