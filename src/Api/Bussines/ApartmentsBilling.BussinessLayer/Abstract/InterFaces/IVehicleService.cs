using ApartmentsBilling.Common.Dtos.VehicleDto;
using ApartmentsBilling.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces
{
    public interface IVehicleService
    {
        Task AddAsync(CreateVehicleDto createVehicleDto);
        Task UpdateAsync(UpdateVehicleDto updateVehicleDto);
        Task RemoveAsync(Guid id);
        Task<List<GetVehicleDto>> GetListWithInclude(Expression<Func<Vehicle, bool>> predicate, bool checkstatus = false, bool tracking = true, Func<IQueryable<Vehicle>, IOrderedQueryable<Vehicle>> orderBy = null, params Expression<Func<Vehicle, object>>[] includes);
        Task<GetVehicleDto> GetSingleWtihInclude(Expression<Func<Vehicle, bool>> predicate, bool checkstatus = false, bool tracking = true, params Expression<Func<Vehicle, object>>[] includes);
    }
}
