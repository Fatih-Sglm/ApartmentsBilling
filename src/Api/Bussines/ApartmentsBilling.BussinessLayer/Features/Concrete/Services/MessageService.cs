using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.DataAccesLayer.Features.Abstract;
using ApartmentsBilling.Entity.Entities;

namespace ApartmentsBilling.BussinessLayer.Features.Concrete.Repositories
{
    public class MeesageService : GenericServices<Message>, IMessageService
    {
        public MeesageService(IGenericRepository<Message> repository) : base(repository)
        {
        }
    }
}
