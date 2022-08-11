using ApartmentsBilling.Common.Dtos.BillTypeDto;
using FluentValidation;

namespace ApartmentsBilling.BussinessLayer.Configuration.Validations.BillTypeValidation
{
    public class CreateBillTypeDtoValidation : AbstractValidator<CreateBillTypeDto>
    {
        public CreateBillTypeDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Fatura Tip Adı Boş Geçilemez");
        }
    }
}
