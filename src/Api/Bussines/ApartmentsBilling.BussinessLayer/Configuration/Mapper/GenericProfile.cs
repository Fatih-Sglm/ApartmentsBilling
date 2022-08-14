using ApartmentsBilling.Common.Dtos.BillsDto;
using ApartmentsBilling.Common.Dtos.BillTypeDto;
using ApartmentsBilling.Common.Dtos.FlatDto;
using ApartmentsBilling.Common.Dtos.MessageDto;
using ApartmentsBilling.Common.Dtos.PaymentDto;
using ApartmentsBilling.Common.Dtos.UserDtos;
using ApartmentsBilling.Common.Dtos.VehicleDto;
using ApartmentsBilling.Entity.Entities;
using AutoMapper;

namespace ApartmentsBilling.BussinessLayer.Mapper
{
    public class GenericProfile : Profile
    {
        public GenericProfile()
        {
            AllowNullDestinationValues = true;
            #region Bill
            CreateMap<Bill, BillDto>().
               ForMember(x => x.FloorNumber, y => y.MapFrom(z => z.Flat.FloorNumber)).
               ForMember(x => x.BillType, y => y.MapFrom(z => z.BillType.Name)).
               ForMember(x => x.BillOwner, y => y.MapFrom(z => z.Flat.User.FullName));

            CreateMap<Bill, CreateBillReceiptDto>().
                ForMember(x => x.BillId, y => y.MapFrom(z => z.Id)).
                ForMember(x => x.Email, y => y.MapFrom(z => z.Flat.User.Email)).
                ForMember(x => x.UserId, y => y.MapFrom(z => z.Flat.User.Id)).
                ForMember(x => x.BillType, y => y.MapFrom(z => z.BillType.Name)).
                ForMember(x => x.Total, y => y.MapFrom(z => z.Price)).
                ForMember(x => x.FullName, y => y.MapFrom(z => z.Flat.User.FullName));

            CreateMap<UpdateBillDto, Bill>();
            #endregion
            #region BillType
            CreateMap<BillType, GetBillTypeDto>();
            CreateMap<CreateBillTypeDto, BillType>();
            CreateMap<UpdateBillTypeDto, BillType>();
            #endregion
            #region Flat
            CreateMap<CreateFlatDto, Flat>();
            CreateMap<UpdateFlatDto, Flat>();
            CreateMap<Flat, GetFlatDto>().
                ForMember(x => x.Tenant_name, y => y.MapFrom(x => x.User.FullName));
            #endregion
            #region Message
            CreateMap<CreateMessageDto, Message>();
            CreateMap<UpdateMessageDto, Message>();
            CreateMap<Message, GetMessageDto>().ForMember(x => x.UserName, y => y.MapFrom(z => z.User.FullName));
            #endregion
            #region User
            CreateMap<CreateUserDto, User>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ReverseMap();
            CreateMap<User, GetUserDto>().
                ForMember(x => x.FloorNumber, y => y.MapFrom(x => x.Flat.FloorNumber)).
                ForMember(x => x.VehicleCount, y => y.MapFrom(z => z.Vehicles.Count));
            #endregion
            #region Vehicle
            CreateMap<CreateVehicleDto, Vehicle>();
            CreateMap<UpdateVehicleDto, Vehicle>();
            CreateMap<Vehicle, GetVehicleDto>().ForMember(x => x.UserId, y => y.MapFrom(z => z.User.Id)).
                ForMember(x => x.UserName, y => y.MapFrom(z => z.User.FullName));
            #endregion
        }
    }
}
