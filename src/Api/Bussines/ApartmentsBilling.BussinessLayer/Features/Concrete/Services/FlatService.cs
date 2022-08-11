using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.DataAccesLayer.Features.Abstract;
using ApartmentsBilling.Entity.Entities;

namespace ApartmentsBilling.BussinessLayer.Features.Concrete.Repositories
{
    public class FlatService : GenericServices<Flat>, IFlatService
    {
        public FlatService(IGenericRepository<Flat> repository) : base(repository)
        {
        }
    }
}
