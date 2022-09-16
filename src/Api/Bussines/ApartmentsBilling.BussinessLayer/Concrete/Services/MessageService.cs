using ApartmentsBilling.BussinessLayer.Configuration.Extension;
using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.MessageDto;
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

    public class MeesageService : IMessageService
    {
        private readonly IMeesageRepository _meesageRepository;
        private readonly IMapper _mapper;

        public MeesageService(IMeesageRepository meesageRepository, IMapper mapper)
        {
            _meesageRepository = meesageRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateMessageDto createMessageDto)
        {
            //await _userService.GetSingleAsync(x => x.Id == createMessageDto.UserId);
            var value = _mapper.Map<Message>(createMessageDto);
            try
            {
                await _meesageRepository.AddAsync(value);
                await _meesageRepository.SaveChangeAsync();
            }
            catch (Exception)
            {
                throw new Exception("Mesaj " + CustomErrorMessage.InsertErrorMessage);
            }
        }

        public async Task<List<GetMessageDto>> GetListWithInclude(Expression<Func<Message, bool>> predicate, bool checkstatus = false, bool tracking = true, Func<IQueryable<Message>, IOrderedQueryable<Message>> orderBy = null, params Expression<Func<Message, object>>[] includes)
        {
            var values = await _meesageRepository.GetListWithInclude(predicate, checkstatus, tracking, orderBy, includes);
            return _mapper.Map<List<GetMessageDto>>(values);
        }

        public async Task<GetMessageDto> GetSingleWtihInclude(Expression<Func<Message, bool>> predicate, bool checkstatus = false, bool tracking = true, params Expression<Func<Message, object>>[] includes)
        {
            var value = await _meesageRepository.GetSingleWtihInclude(predicate, checkstatus, tracking, includes);
            value.IsRead = true;
            _meesageRepository.Update(value);
            await _meesageRepository.SaveChangeAsync();
            return _mapper.Map<GetMessageDto>(value);
        }

        public async Task RemoveAsync(Guid id)
        {
            var value = await _meesageRepository.GetSingleAsync(x => x.Id == id, true);
            try
            {
                _meesageRepository.Remove(value);
                await _meesageRepository.SaveChangeAsync();
            }
            catch (Exception)
            {
                throw new Exception("Mesaj " + CustomErrorMessage.DeleteErrorMessage);
            }
        }

        public async Task UpdateAsync(UpdateMessageDto updateMessageDto)
        {
            var value = await _meesageRepository.GetSingleAsync(x => x.Id == updateMessageDto.Id);
            try
            {
                _meesageRepository.Update(_mapper.Map(updateMessageDto, value));
                await _meesageRepository.SaveChangeAsync();
            }
            catch (Exception)
            {
                throw new Exception("Mesaj " + CustomErrorMessage.UpdatetErrorMessage);
            }
        }
    }
}
