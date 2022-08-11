using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.DataAccesLayer.Features.Abstract;
using ApartmentsBilling.Entity.Entities;

namespace ApartmentsBilling.BussinessLayer.Features.Concrete.Repositories
{
    public class BillTypeService : GenericServices<BillType>, IBillTypeService
    {
        public BillTypeService(IGenericRepository<BillType> repository) : base(repository)
        {
        }
    }
}
