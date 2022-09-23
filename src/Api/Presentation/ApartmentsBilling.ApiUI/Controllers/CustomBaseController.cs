using ApartmentsBilling.BussinessLayer.Configuration.LogFilters;
using ApartmentsBilling.Common.Dtos.CustomDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentsBilling.ApiUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [LogFilter]
    //[Authorize]
    //[AllowAnonymous]
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreatActionResult<GetBillTypeDto>(CustomResponseDto<GetBillTypeDto> dto)
        {
            return new ObjectResult(dto)
            {
                StatusCode = Response.StatusCode,
            };
        }
    }
}
