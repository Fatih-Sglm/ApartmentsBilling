﻿using ApartmentsBilling.Common.Dtos.BillTypeDto;
using ApartmentsBilling.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces
{
    public interface IBillTypeService
    {
        Task AddAsync(CreateBillTypeDto createBillTypeDto);
        Task AddRangeAsync(List<CreateBillTypeDto> createBillTypeDtos);
        Task UpdateAsync(UpdateBillTypeDto updateBillTypeDto);
        Task RemoveAsync(Guid id);
        List<GetBillTypeDto> GetAll(Func<IQueryable<BillType>, IOrderedQueryable<BillType>> orderBy = null, bool checkstatus = false, bool tracking = true);
        Task<GetBillTypeDto> GetSingleAsync(Expression<Func<BillType, bool>> expression, bool checkstatus = false, bool tracking = true);
    }
}
