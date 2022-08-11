using ApartmentsBilling.Common.Dtos.BillTypeDto;
using FluentValidation;

namespace ApartmentsBilling.BussinessLayer.Configuration.Validations.BillTypeValidation
{
    public class UpdateBillTypeDtoValidation : AbstractValidator<UpdateBillTypeDto>
    {
        public UpdateBillTypeDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Fatura Tip Adı Boş Geçilemez");
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id Kısmı Boş Bırakılamaz");
        }
    }
}
