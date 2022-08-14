using ApartmentsBilling.BussinessLayer.Configuration.Extension;
using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.VehicleDto;
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
    public class VehicleService : IVehicleService
    {
        private readonly IMapper _mapper;

        private readonly IVehicleRepository _vehicleRepo;
        private readonly IUserRepository _userRepository;

        public VehicleService(IMapper mapper, IVehicleRepository vehicleRepo, IUserRepository userRepository)
        {
            _mapper = mapper;
            _vehicleRepo = vehicleRepo;
            _userRepository = userRepository;
        }

        public async Task<bool> AddAsync(CreateVehicleDto createVehicleDto)
        {
            try
            {
                await _userRepository.GetSingleAsync(x => x.Id == createVehicleDto.UserId, true, false);
                var vehicle = _mapper.Map<Vehicle>(createVehicleDto);
                await _vehicleRepo.AddAsync(vehicle);
                await _vehicleRepo.SaveChangeAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Araç " + CustomErrorMessage.InsertErrorMessage + "\n" + ex.Message);

            }
        }

        public async Task<List<GetVehicleDto>> GetListWithInclude(Expression<Func<Vehicle, bool>> predicate, bool checkstatus = false, bool tracking = true, Func<IQueryable<Vehicle>, IOrderedQueryable<Vehicle>> orderBy = null, params Expression<Func<Vehicle, object>>[] includes)
        {
            var vehicles = await _vehicleRepo.GetListWithInclude(predicate, checkstatus, tracking, orderBy, includes);
            return _mapper.Map<List<GetVehicleDto>>(vehicles);
        }
        public async Task<GetVehicleDto> GetSingleWtihInclude(Expression<Func<Vehicle, bool>> predicate, bool checkstatus = false, bool tracking = true, params Expression<Func<Vehicle, object>>[] includes)
        {
            var vehicle = await _vehicleRepo.GetSingleWtihInclude(predicate, checkstatus, tracking, includes);
            return _mapper.Map<GetVehicleDto>(vehicle);
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            try
            {
                var vehicle = await _vehicleRepo.GetSingleAsync(x => x.Id == id, true);
                _vehicleRepo.Remove(vehicle);
                await _vehicleRepo.SaveChangeAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Araç " + CustomErrorMessage.DeleteErrorMessage + "\n" + ex.Message);

            }
        }

        public async Task<bool> UpdateAsync(UpdateVehicleDto updateVehicleDto)
        {
            try
            {
                var vehicle = await _vehicleRepo.GetSingleAsync(x => x.Id == updateVehicleDto.Id, true);
                _mapper.Map(updateVehicleDto, vehicle);
                _vehicleRepo.Update(vehicle);
                await _vehicleRepo.SaveChangeAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Araç " + CustomErrorMessage.DeleteErrorMessage + "\n" + ex.Message);
            }
        }
    }
}
