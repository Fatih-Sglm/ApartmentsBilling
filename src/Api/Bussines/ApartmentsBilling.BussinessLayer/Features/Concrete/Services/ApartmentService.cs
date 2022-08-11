using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.DataAccesLayer.Features.Abstract;
using ApartmentsBilling.Entity.Entities;

namespace ApartmentsBilling.BussinessLayer.Features.Concrete.Repositories
{
    public class ApartmentService : GenericServices<Apartment>, IApartmentService
    {
        public ApartmentService(IGenericRepository<Apartment> repository) : base(repository)
        {
        }
    }
}
