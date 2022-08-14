using ApartmentsBilling.DataAccesLayer.Abstract.InterFaces;
using ApartmentsBilling.DataAccesLayer.Contexts;
using ApartmentsBilling.DataAccesLayer.Features.Concrete.Common;
using ApartmentsBilling.Entity.Entities;

namespace ApartmentsBilling.DataAccesLayer.Concrete.Repositories
{
    public class BillRepository : GenericRepository<Bill>, IBillRepository
    {
        public BillRepository(ApartmentDbContext dbContext) : base(dbContext)
        {
        }
    }
}
