using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.BillsDto;
using ApartmentsBilling.DataAccesLayer.Features.Abstract;
using ApartmentsBilling.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Features.Concrete.Repositories
{
    public class BillService : GenericServices<Bill>, IBillService
    {
        private readonly IBillTypeService _billTypeService;
        private readonly IFlatService _flatService;
        private readonly IApartmentService _apartmentService;


        public BillService(IGenericRepository<Bill> repository, IBillTypeService billTypeService, IFlatService flatService, IApartmentService apartmentService) : base(repository)
        {
            _billTypeService = billTypeService;
            _flatService = flatService;
            _apartmentService = apartmentService;
        }


        public async Task<bool> CreateBill(List<CreateBillDto> dto)
        {
            List<Bill> blist = new();
            foreach (var entity in dto)
            {
                await _apartmentService.GetSingleAsync(x => x.Id == entity.ApartmentId, true);
                var billtype = await _billTypeService.GetSingleAsync(x => x.Id == entity.BilltypeId, true);
                var flats = await _flatService.GetListWithInclude(x => x.ApartmentId == entity.ApartmentId);

                foreach (var item in flats)
                {
                    Bill b = new()
                    {
                        BillTypeId = entity.BilltypeId,
                        IsPayment = false,
                        Price = entity.TotalPrice / flats.Count,
                        FlatId = item.Id,
                        LastPaymentDate = entity.LastPaymentDate,
                    };
                    blist.Add(b);
                }
            }

            await _repository.AddRangeAsync(blist);
            await _repository.SaveChangeAsync();
            return true;
        }


    }
}
