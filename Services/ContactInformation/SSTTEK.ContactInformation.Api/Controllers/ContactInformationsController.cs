using EntityBase.Concrete;
using EntityBase.Poco.Responses;
using Microsoft.AspNetCore.Mvc;
using RestHelpers.Controllers;
using SSTTEK.ContactInformation.Entities.Poco.ContactInformationDto;

namespace SSTTEK.ContactInformation.Api.Controllers
{
    [Route("api/[controller]")]
    public class ContactInformationsController : ApiControllerBase
    {
        [HttpPost("create")]
        public IActionResult Create(CreateContactInformationRequest request)
        {
            return CreateActionResult(Response<CreateContactInformationRequest>.Fail("Metot Not Implemented", 400));
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

        [HttpGet("getreport")]
        public IActionResult GetReport()
        {
            return CreateActionResult(Response<FilterModel>.Fail("Metot Not Implemented", 400));
        }
    }
}