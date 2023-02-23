using ApartmentsBilling.BussinessLayer.Configuration.Extension;
using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.BillsDto;
using ApartmentsBilling.DataAccesLayer.Abstract.InterFaces;
using ApartmentsBilling.DataAccesLayer.InterFaces;
using ApartmentsBilling.Entity.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Features.Concrete.Repositories
{
    public class BillService : IBillService
    {
        private readonly IFlatRepository _flatRepository;
        private readonly IBillRepository _billRepository;
        private readonly IApartmentRepository _apartRepo;
        private readonly IMapper _mapper;
        private readonly IBillTypeRepository _billTypeRepository;

        public BillService(
            IBillRepository billRepository, IApartmentRepository apartRepo, IMapper mapper, IFlatRepository flatRepository, IBillTypeRepository billTypeRepository)
        {
            _billRepository = billRepository;
            _apartRepo = apartRepo;
            _mapper = mapper;
            _flatRepository = flatRepository;
            _billTypeRepository = billTypeRepository;
        }

        public async Task<bool> AddRangeAsync(List<CreateBillDto> dto)
        {
            try
            {
                List<Bill> blist = new();
                foreach (var entity in dto)
                {
                    await _apartRepo.GetSingleAsync(x => x.Id == entity.ApartmentId, true);
                    var billtype = await _billTypeRepository.GetSingleAsync(x => x.Id == entity.BilltypeId, true, false);
                    var flats = await _flatRepository.GetListWithInclude(x => x.ApartmentId == entity.ApartmentId, true, includes: x => x.User);

                    foreach (var item in flats.Where(x => x.User != null))
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
                await _billRepository.AddRangeAsync(blist);
                await _billRepository.SaveChangeAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Fatura" + CustomErrorMessage.InsertErrorMessage + "\n" + ex.Message);
            }
        }

        public async Task<List<BillDto>> GetListWithInclude(Expression<Func<Bill, bool>> predicate, bool checkstatus = false, bool tracking = true, Func<IQueryable<Bill>, IOrderedQueryable<Bill>> orderBy = null, params Expression<Func<Bill, object>>[] includes)
        {
            return _mapper.Map<List<BillDto>>(await _billRepository.GetListWithInclude(predicate, checkstatus, tracking, orderBy, includes));
        }
        public async Task<BillDto> GetSingleAsync(Expression<Func<Bill, bool>> expression, bool checkstatus = false, bool tracking = true)
        {
            var v = await _billRepository.GetSingleAsync(expression, checkstatus, tracking);
            return _mapper.Map<BillDto>(v);
        }
        public async Task<bool> RemoveAsync(Guid id)
        {
            var v = await _billRepository.GetSingleAsync(x => x.Id == id);
            try
            {
                _billRepository.Remove(v);
                await _billRepository.SaveChangeAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Bu Fatura " + CustomErrorMessage.DeleteErrorMessage + "\n" + ex.Message);
            }
        }
        public async Task<bool> UpdateAsync(UpdateBillDto updateBillDto)
        {
            await _billRepository.GetSingleAsync(x => x.Id == updateBillDto.Id, true);
            await _flatRepository.GetSingleAsync(x => x.Id == updateBillDto.FlatId, true, false);
            await _billTypeRepository.GetSingleAsync(x => x.Id == updateBillDto.BillTypeId, true, false);
            var value = _mapper.Map(updateBillDto, await _billRepository.GetSingleAsync(x => x.Id == updateBillDto.Id, true));
            try
            {
                _billRepository.Update(_mapper.Map<Bill>(value));
                await _billRepository.SaveChangeAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception("Fatura " + CustomErrorMessage.UpdatetErrorMessage + "\n" + ex.Message); }
        }
    }
}
