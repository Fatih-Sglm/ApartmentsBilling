using ApartmentsBilling.Common.Dtos.FlatDto;
using ApartmentsBilling.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces
{
    public interface IFlatService
    {
        Task<bool> AddAsync(CreateFlatDto createFlatDto);
        Task<bool> AddRangeAsync(List<CreateFlatDto> createFlatDtos);
        Task<bool> UpdateAsync(UpdateFlatDto updateFlatDto);
        Task<bool> RemoveAsync(Guid id);

        Task<List<GetFlatDto>> GetListWithInclude(Expression<Func<Flat, bool>> predicate, bool checkstatus = false, bool tracking = true, Func<IQueryable<Flat>, IOrderedQueryable<Flat>> orderBy = null, params Expression<Func<Flat, object>>[] includes);
        Task<GetFlatDto> GetSingleAsync(Expression<Func<Flat, bool>> expression, bool checkstatus = false, bool tracking = true);
    }
}
