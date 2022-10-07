using EntityBase.Concrete;
using EntityBase.Poco.Responses;
using Microsoft.AspNetCore.Mvc;
using RestHelpers.Controllers;
using SSTTEK.Location.Entities.Poco.Location;

namespace SSTTEK.LocationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ApiControllerBase
    {
        [HttpPost("create")]
        public IActionResult Create(CreateLocationRequest request)
        {
            return CreateActionResult(Response<CreateLocationRequest>.Fail("Metot Not Implemented", 400));
        }

        [HttpPut("update")]
        public IActionResult Update(UpdateLocationRequest request)
        {
            return CreateActionResult(Response<UpdateLocationRequest>.Fail("Metot Not Implemented", 400));
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
