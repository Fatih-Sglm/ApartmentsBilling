using ApartmentsBilling.Common.Dtos.PaymentDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApartmentsBilling.PaymentApiSevices.Services.Concrete
{
    public interface IReceiptService
    {
        Task CreatePayment(PaymentDto paymentDto);
        Task<List<GetReceiptDto>> GetAllReceipt(string userId);
        Task<GetReceiptDto> GetSingleReceipt(string id);
        Task Remove(string id);
    }
}