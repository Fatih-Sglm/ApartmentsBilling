using ApartmentsBilling.Common.Dtos.BillsDto;
using FluentValidation;

namespace ApartmentsBilling.BussinessLayer.Configuration.Validations.BillValidation
{
    public class UpdateBillDtoValidation : AbstractValidator<UpdateBillDto>
    {
        public UpdateBillDtoValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Fatura  Id si Boş Geçilemez");
            RuleFor(x => x.FlatId).NotEmpty().WithMessage("Daire  Id si Boş Geçilemez");
            RuleFor(x => x.BillTypeId).NotEmpty().WithMessage("Fatura Tip Id si Boş Geçilemez");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat Kısmı Boş Geçilemez")
                .GreaterThan(0).WithMessage("Fiyat 0 DanBüyük Olmak Zorundadır!");
        }
    }
}
