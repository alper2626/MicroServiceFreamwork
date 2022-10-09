using Microsoft.AspNetCore.Mvc;
using RestHelpers.Controllers;
using SSTTEK.ContactInformation.Business.Contracts;

namespace SSTTEK.ContactInformation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInformationReportsController : ApiControllerBase
    {
        IContactInformationReportService _contactInformationReportService;

        public ContactInformationReportsController(IContactInformationReportService contactInformationReportService)
        {
            _contactInformationReportService = contactInformationReportService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            throw new NotImplementedException();
            //return CreateActionResult(await _contactInformationReportService.GetLocationBasedReport());
        }
    }
}
