using EntityBase.Concrete;
using Microsoft.AspNetCore.Mvc;
using RestHelpers.Controllers;
using SSTTEK.ContactInformation.Business.Contracts;
using SSTTEK.ContactInformation.Entities.Poco.ContactInformationDto;

namespace SSTTEK.ContactInformation.Api.Controllers
{
    [Route("api/[controller]")]
    public class ContactInformationsController : ApiControllerBase
    {
        IContactInformationService _contactInformationService;

        public ContactInformationsController(IContactInformationService contactInformationService)
        {
            _contactInformationService = contactInformationService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateContactInformationRequest request)
        {
            return CreateActionResult(await _contactInformationService.Create(request));
        }

        [HttpPut("remove")]
        public async Task<IActionResult> Remove(FilterModel request)
        {
            return CreateActionResult(await _contactInformationService.Remove(request));
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(FilterModel request)
        {
            return CreateActionResult(await _contactInformationService.Get(request));
        }

    }
}