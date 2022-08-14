using ApartmentsBilling.DataAccesLayer.Abstract.InterFaces;
using ApartmentsBilling.DataAccesLayer.Contexts;
using ApartmentsBilling.DataAccesLayer.Features.Concrete.Common;
using ApartmentsBilling.Entity.Entities;

namespace ApartmentsBilling.DataAccesLayer.Concrete.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMeesageRepository
    {
        public MessageRepository(ApartmentDbContext dbContext) : base(dbContext)
        {
        }
    }
}
