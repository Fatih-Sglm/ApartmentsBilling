using ApartmentsBilling.BussinessLayer.Configuration.Cache;
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
        private readonly ICache_Helper _cache_Helper;

        public BillTypeService(IBillTypeRepository billTypeRepository, IMapper mapper, ICache_Helper cache_Helper)
        {
            _billTypeRepository = billTypeRepository;
            _mapper = mapper;
            _cache_Helper = cache_Helper;
        }

        public async Task AddAsync(CreateBillTypeDto createBillTypeDto)
        {
            var value = _mapper.Map<BillType>(createBillTypeDto);
            try
            {
                await _billTypeRepository.AddAsync(value);
                await _billTypeRepository.SaveChangeAsync();
                await _cache_Helper.AddCache(CacheKeys.GetAllBillTypeKey, _mapper.Map<List<GetBillTypeDto>>(await _billTypeRepository.GetAll()));
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
                //await _cache_Helper.AddCache(CacheKeys.GetAllBillTypeKey, _mapper.Map<List<GetBillTypeDto>>(await _billTypeRepository.GetAll(null, null, true, false)));
            }
            catch (Exception)
            {
                throw new Exception("Fatura Tipleri " + CustomErrorMessage.InsertErrorMessage);
            }
        }
        public async Task<List<GetBillTypeDto>> GetAll(Expression<Func<BillType, bool>> predicate = null, Func<IQueryable<BillType>, IOrderedQueryable<BillType>> orderBy = null, bool checkstatus = false, bool tracking = true)
        {
            var value = await _cache_Helper.GetValue<List<GetBillTypeDto>>(CacheKeys.GetAllBillTypeKey);
            if (value == null)
            {
                await _cache_Helper.AddCache(CacheKeys.GetAllBillTypeKey, _mapper.Map<List<GetBillTypeDto>>(await _billTypeRepository.GetAll(null, orderBy, checkstatus, tracking)));
                //value = await _cache_Helper.GetValue<List<GetBillTypeDto>>(CacheKeys.GetAllBillTypeKey);
            }
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
            catch (Exception)
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
