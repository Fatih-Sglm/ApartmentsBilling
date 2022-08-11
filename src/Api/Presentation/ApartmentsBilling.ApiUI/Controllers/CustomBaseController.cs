using ApartmentsBilling.BussinessLayer.Configuration.LogFilters;
using ApartmentsBilling.Common.Dtos.CustomDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace ApartmentsBilling.ApiUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [LogFilter]
    //[Authorize]
    [AllowAnonymous]
    public class CustomBaseController : ControllerBase
    {
        protected const string admin = "Admin";
        protected const string user = "User";

        public IActionResult CreatActionResult<T>(CustomResponseDto<T> dto)
        {
            return new ObjectResult(dto)
            {
                StatusCode = Response.StatusCode,
            };
        }

        public class AppUser
        {
            public string Id { get; set; }
            public string Role { get; set; }
        }

        protected new AppUser User()
        {
            HttpContextAccessor httpContextAccessor = new();
            var userClaims = httpContextAccessor.HttpContext.User.Claims;
            AppUser appUser = new()
            {
                Id = userClaims?.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value,
                Role = userClaims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value
            };
            if (appUser != null)
                return appUser;
            else
                throw new Exception("Bu Sayfaya Giriş Yetkiniz Bulunmamaktadır");
        }

        protected bool IsAuthorize()
        {
            HttpContextAccessor httpContextAccessor = new();
            var userClaims = httpContextAccessor.HttpContext.User.Claims;
            if (userClaims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value == "Admin")
                return true;
            else
                return false;
        }


    }
}
