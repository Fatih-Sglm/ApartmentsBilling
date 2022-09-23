using ApartmentsBilling.BussinessLayer.Configuration.LogFilters;
using ApartmentsBilling.Common.Dtos.CustomDto;
<<<<<<<<< Temporary merge branch 1
using Microsoft.AspNetCore.Http;
=========
>>>>>>>>> Temporary merge branch 2
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
