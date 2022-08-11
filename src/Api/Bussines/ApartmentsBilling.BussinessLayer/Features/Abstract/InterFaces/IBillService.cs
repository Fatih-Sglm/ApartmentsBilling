using ApartmentsBilling.Common.Dtos.BillsDto;
using ApartmentsBilling.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces
{
    public interface IBillService : IService<Bill>
    {
        Task<bool> CreateBill(List<CreateBillDto> dto);

    }
}
