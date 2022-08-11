using ApartmentsBilling.Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace ApartmentsBilling.ApiUI.Filters
{
    public class PermissionFilter : IAuthorizationFilter
    {
        private readonly UserRole _userrole;

        public PermissionFilter(UserRole userrole)
        {
            _userrole = userrole;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var role = context.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;

            if (role != _userrole.ToString())
            {
                context.Result = new ForbidResult();
                //context.Result = new CustomUnauthorizedResult(CustomResponseDto<NoContent>.Fail(403, "BU Alana Giriş için Yetkiniz Bulunmamaktadır"));
            }
        }
    }
}