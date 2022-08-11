using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.DataAccesLayer.Features.Abstract;
using ApartmentsBilling.Entity.Entities;

namespace ApartmentsBilling.BussinessLayer.Features.Concrete.Repositories
{
    public class VehicleService : GenericServices<Vehicle>, IVehicleService
    {
        public VehicleService(IGenericRepository<Vehicle> repository) : base(repository)
        {
        }
    }
}
