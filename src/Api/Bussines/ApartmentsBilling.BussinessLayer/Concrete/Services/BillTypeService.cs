using ApartmentsBilling.BussinessLayer.Configuration.Extension;
using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.BillTypeDto;
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
    public class BillTypeService : IBillTypeService
    {
        private readonly IBillTypeRepository _billTypeRepository;
        private readonly IMapper _mapper;

        public BillTypeService(IBillTypeRepository billTypeRepository, IMapper mapper)
        {
            _billTypeRepository = billTypeRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateBillTypeDto createBillTypeDto)
        {
            var value = _mapper.Map<BillType>(createBillTypeDto);
            try
            {
                await _billTypeRepository.AddAsync(value);
                await _billTypeRepository.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Fatura Tipi " + CustomErrorMessage.InsertErrorMessage + "\n" + ex.Message);
            }
        }
        public async Task AddRangeAsync(List<CreateBillTypeDto> createBillTypeDtos)
        {
            var value = _mapper.Map<List<BillType>>(createBillTypeDtos);
            try
            {
                await _billTypeRepository.AddRangeAsync(value);
                await _billTypeRepository.SaveChangeAsync();
            }
            catch (Exception)
            {
                throw new Exception("Fatura Tipleri " + CustomErrorMessage.InsertErrorMessage);
            }
        }
        public async Task<List<GetBillTypeDto>> GetAll(Expression<Func<BillType, bool>> predicate = null, Func<IQueryable<BillType>, IOrderedQueryable<BillType>> orderBy = null, bool checkstatus = false, bool tracking = true)
        {
            var billtype = await _billTypeRepository.GetAll(null, orderBy, checkstatus, tracking);
            var value = _mapper.Map<List<GetBillTypeDto>>(billtype);
            return value;
        }
        public async Task<GetBillTypeDto> GetSingleAsync(Expression<Func<BillType, bool>> expression, bool checkstatus = false, bool tracking = true)
        {
            var value = await _billTypeRepository.GetSingleAsync(expression, checkstatus, tracking);
            return _mapper.Map<GetBillTypeDto>(value);
        }
        public async Task RemoveAsync(Guid id)
        {
            var value = await _billTypeRepository.GetSingleAsync(x => x.Id == id, true);
            try
            {
                _billTypeRepository.Remove(value);
                await _billTypeRepository.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Fatura Tipi " + CustomErrorMessage.DeleteErrorMessage);
            }
        }
        public async Task UpdateAsync(UpdateBillTypeDto updateBillTypeDto)
        {
            var value = await _billTypeRepository.GetSingleAsync(x => x.Id == updateBillTypeDto.Id);
            try
            {
                _billTypeRepository.Update(_mapper.Map(updateBillTypeDto, value));
                await _billTypeRepository.SaveChangeAsync();
            }
            catch (Exception)
            {
                throw new Exception("Fatura Tipi " + CustomErrorMessage.UpdatetErrorMessage);
            }
        }
    }
}
