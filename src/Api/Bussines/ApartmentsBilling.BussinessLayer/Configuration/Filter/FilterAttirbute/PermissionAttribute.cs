using ApartmentsBilling.ApiUI.Filters;
using ApartmentsBilling.Entity.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentsBilling.BussinessLayer.Configuration.Filter.FilterAttirbute
{
    public class PermissionAttribute : TypeFilterAttribute
    {
        public PermissionAttribute(UserRole userRole) : base(typeof(PermissionFilter))
        {
            Arguments = new object[] { userRole };
        }
    }
}
