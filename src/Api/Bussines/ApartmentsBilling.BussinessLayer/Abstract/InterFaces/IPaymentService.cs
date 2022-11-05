using ApartmentsBilling.Common.Dtos.PaymentDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Abstract.InterFaces
{
    public interface IPaymentService
    {
        Task CreatePayment(BillPaymentDto billPaymentDto);
        Task<List<GetReceiptDto>> GetListAsync();
        Task<GetReceiptDto> GetSingleAsync(string id);
        Task RemoveAsync(string id);
    }
}
