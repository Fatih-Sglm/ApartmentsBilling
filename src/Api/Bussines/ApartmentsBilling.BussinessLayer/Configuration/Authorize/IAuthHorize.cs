using static ApartmentsBilling.BussinessLayer.Configuration.Authorize.AuthHorize;

namespace ApartmentsBilling.BussinessLayer.Configuration.Authorize
{
    public interface IAuthHorize
    {
        AppUser User();
        bool IsAuthorize();
    }
}
