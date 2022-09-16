using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ApartmentsBilling.BussinessLayer.Configuration.Authorize
{
    public class AuthHorize : IAuthHorize
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        IEnumerable<Claim> _userClaims;

        public AuthHorize(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _userClaims = _httpContextAccessor.HttpContext.User.Claims;
        }
        public bool IsAuthorize()
        {
            if (_userClaims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value == "Admin")
                return true;
            else
                return false;
        }

        public AppUser User()
        {
            AppUser appUser = new()
            {
                Id = _userClaims?.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value,
                Role = _userClaims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value
            };
            if (appUser != null)
                return appUser;
            else
                throw new Exception("Bu Sayfaya Giriş Yetkiniz Bulunmamaktadır");
        }

        public class AppUser
        {
            public string Id { get; set; }
            public string Role { get; set; }
        }


    }
}
