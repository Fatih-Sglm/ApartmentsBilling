using ApartmentsBilling.BussinessLayer.Configuration.Extension;
using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.FlatDto;
using ApartmentsBilling.DataAccesLayer.Abstract.InterFaces;
using ApartmentsBilling.Entity.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Features.Concrete.Repositories
{
    public class FlatService : IFlatService
    {
        private readonly IFlatRepository _flatRepository;
        private readonly IMapper _mapper;

        public FlatService(IFlatRepository flatRepository, IMapper mapper)
        {
            _flatRepository = flatRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(CreateFlatDto createFlatDto)
        {
            var value = _mapper.Map<Flat>(createFlatDto);
            try
            {
                await _flatRepository.AddAsync(value);
                await _flatRepository.SaveChangeAsync();
                return true;
            }
            catch (Exception)
            {
                throw new Exception("Daire " + CustomErrorMessage.InsertErrorMessage);
            }
        }
        public async Task<bool> AddRangeAsync(List<CreateFlatDto> createFlatDtos)
        {
            var value = _mapper.Map<List<Flat>>(createFlatDtos);
            try
            {
                await _flatRepository.AddRangeAsync(value);
                await _flatRepository.SaveChangeAsync();
                return true;
            }
            catch (Exception)
            {
                throw new Exception("Daireler " + CustomErrorMessage.InsertErrorMessage);
            }
        }
        public async Task<List<GetFlatDto>> GetListWithInclude(Expression<Func<Flat, bool>> predicate, bool checkstatus = false, bool tracking = true, Func<IQueryable<Flat>, IOrderedQueryable<Flat>> orderBy = null, params Expression<Func<Flat, object>>[] includes)
        {
            var value = await _flatRepository.GetListWithInclude(predicate, checkstatus, tracking, orderBy, includes);
            return _mapper.Map<List<GetFlatDto>>(value);
        }
        public async Task<GetFlatDto> GetSingleAsync(Expression<Func<Flat, bool>> expression, bool checkstatus = false, bool tracking = true)
        {
            var flat = await _flatRepository.GetSingleAsync(expression, checkstatus, tracking);
            return _mapper.Map<GetFlatDto>(flat);
        }
        public async Task<bool> RemoveAsync(Guid id)
        {
            var flat = await _flatRepository.GetSingleAsync(x => x.Id == id, true);
            try
            {
                _flatRepository.Remove(flat);
                await _flatRepository.SaveChangeAsync();
                return true;
            }
            catch (Exception)
            {
                throw new Exception("Daire " + CustomErrorMessage.DeleteErrorMessage);
            }
        }
        public async Task<bool> UpdateAsync(UpdateFlatDto updateFlatDto)
        {
            var value = await _flatRepository.GetSingleAsync(x => x.Id == updateFlatDto.Id, true);
            try
            {
                _flatRepository.Update(_mapper.Map(updateFlatDto, value));
                await _flatRepository.SaveChangeAsync();
                return true;
            }
            catch (Exception)
            {
                throw new Exception("Daire" + CustomErrorMessage.UpdatetErrorMessage);
            }
        }
    }
}