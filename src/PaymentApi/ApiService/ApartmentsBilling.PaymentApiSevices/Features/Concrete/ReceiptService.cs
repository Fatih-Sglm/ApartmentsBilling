using ApartmentsBilling.PaymentApiSevices.DBSettings;
using ApartmentsBilling.PaymentApiSevices.Entities;
using ApartmentsBilling.PaymentApiSevices.Features.Abstract;
using ApartmentsBilling.PaymentApiSevices.Features.Concrete.common;

namespace ApartmentsBilling.PaymentApiSevices.Features.Concrete
{
    public class ReceiptService : GenericService<Receipt>, IReceiptService
    {
        public ReceiptService(IDbSettings dbSettings) : base(dbSettings)
        {
        }
    }
}
