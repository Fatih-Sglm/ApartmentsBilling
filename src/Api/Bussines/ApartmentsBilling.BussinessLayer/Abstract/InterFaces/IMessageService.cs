using ApartmentsBilling.Common.Dtos.MessageDto;
using ApartmentsBilling.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces
{
    public interface IMessageService
    {
        Task<bool> AddAsync(CreateMessageDto createMessageDto);
        Task<bool> UpdateAsync(UpdateMessageDto updateMessageDto);
        Task<bool> RemoveAsync(Guid id);
        Task<List<GetMessageDto>> GetListWithInclude(Expression<Func<Message, bool>> predicate, bool checkstatus = false, bool tracking = true, Func<IQueryable<Message>, IOrderedQueryable<Message>> orderBy = null, params Expression<Func<Message, object>>[] includes);

        Task<GetMessageDto> GetSingleWtihInclude(Expression<Func<Message, bool>> predicate, bool checkstatus = false, bool tracking = true, params Expression<Func<Message, object>>[] includes);


    }
}
