using ApartmentsBilling.PaymentApiSevices.DBSettings;
using ApartmentsBilling.PaymentApiSevices.Entities;
using ApartmentsBilling.PaymentApiSevices.Repositories.Concrete.Common;
using ApartmentsBilling.PaymentApiSevices.Repositories.Interfaces;

namespace ApartmentsBilling.PaymentApiSevices.Repositories
{
    public class ReceipRepository : GenericRepository<Receipt>, IReceipRepository
    {
        public ReceipRepository(IDbSettings dbSettings) : base(dbSettings)
        {
        }
    }
}
