using ApartmentsBilling.Common.Dtos.BillsDto;
using ApartmentsBilling.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces
{
    public interface IBillService
    {
        //Task<bool> AddAsync(CreateBillDto createBillDto);
        Task<bool> AddRangeAsync(List<CreateBillDto> dto);
        Task<bool> UpdateAsync(UpdateBillDto updateBillDto);
        Task<bool> RemoveAsync(Guid id);
        Task<List<BillDto>> GetListWithInclude(Expression<Func<Bill, bool>> predicate, bool checkstatus = false, bool tracking = true, Func<IQueryable<Bill>, IOrderedQueryable<Bill>> orderBy = null, params Expression<Func<Bill, object>>[] includes);
        Task<BillDto> GetSingleAsync(Expression<Func<Bill, bool>> expression, bool checkstatus = false, bool tracking = true);
    }
}
