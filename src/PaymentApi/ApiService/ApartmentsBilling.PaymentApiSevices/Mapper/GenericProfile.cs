using ApartmentsBilling.Common.Dtos.PaymentDto;
using ApartmentsBilling.PaymentApiSevices.Entities;
using AutoMapper;

namespace ApartmentsBilling.PaymentApiSevices.Mapper
{
    public class GenericProfile : Profile
    {
        public GenericProfile()
        {
            CreateMap<CreateBillReceiptDto, Receipt>().ReverseMap();
            CreateMap<Receipt, GetReceiptDto>().ForMember(x => x.PaymentDate, y => y.MapFrom(x => x.CreatedAt));
        }
    }
}
