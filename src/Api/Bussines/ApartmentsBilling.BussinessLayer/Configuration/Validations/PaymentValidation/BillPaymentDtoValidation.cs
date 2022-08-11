using ApartmentsBilling.Common.Dtos.PaymentDto;
using FluentValidation;

namespace ApartmentsBilling.BussinessLayer.Configuration.Validations.PaymentValidation
{
    public class BillPaymentDtoValidation : AbstractValidator<BillPaymentDto>
    {
        public BillPaymentDtoValidation()
        {
            RuleFor(x => x.BillId).NotEmpty().WithMessage("Id Boş Geçilemez");
            RuleFor(x => x.CardNumber).NotEmpty().WithMessage("Kart Numarası Boş Geçilemez").CreditCard();
            RuleFor(x => x.Cv2).NotEmpty().WithMessage("Cv2 Boş Geçilemez");
            RuleFor(x => x.LastUseDate).NotEmpty().WithMessage("Son Kullanım Tarihi Boş Geçilemez");
        }
    }
}
