using EntityBase.Concrete;
using Microsoft.AspNetCore.Mvc;
using RestHelpers.Controllers;
using SSTTEK.Location.Business.Contracts;
using SSTTEK.Location.Entities.Poco.Location;

namespace SSTTEK.Location.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ApiControllerBase
    {
        ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateLocationRequest request)
        {
            return CreateActionResult(await _locationService.Create(request));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateLocationRequest request)
        {
            return CreateActionResult(await _locationService.Update(request));
        }

        [HttpPost("get")]
        public async Task<IActionResult> List()
        {
            return CreateActionResult(await _locationService.GetList());
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CreateActionResult(await _locationService.Delete(id));
        }
    }
}
