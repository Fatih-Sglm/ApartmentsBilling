using ApartmentsBilling.DataAccesLayer.Contexts;
using ApartmentsBilling.DataAccesLayer.Features.Concrete.Common;
using ApartmentsBilling.DataAccesLayer.InterFaces;
using ApartmentsBilling.Entity.Entities;

namespace ApartmentsBilling.DataAccesLayer.Concrete.Repositories
{
    public class BillTypeRepository : GenericRepository<BillType>, IBillTypeRepository
    {
        public BillTypeRepository(ApartmentDbContext dbContext) : base(dbContext)
        {
        }
    }
}
