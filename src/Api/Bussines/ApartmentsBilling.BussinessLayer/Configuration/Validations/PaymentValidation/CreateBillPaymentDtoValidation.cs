using ApartmentsBilling.Common.Dtos.PaymentDto;
using FluentValidation;

namespace ApartmentsBilling.BussinessLayer.Configuration.Validations.PaymentValidation
{
    public class CreateBillPaymentDtoValidation : AbstractValidator<CreateBillReceiptDto>
    {
        public CreateBillPaymentDtoValidation()
        {

        }
    }
}
