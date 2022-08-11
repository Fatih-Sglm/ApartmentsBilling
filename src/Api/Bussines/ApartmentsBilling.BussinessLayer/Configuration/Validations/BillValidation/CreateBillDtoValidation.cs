using ApartmentsBilling.Common.Dtos.BillsDto;
using FluentValidation;
using System;

namespace ApartmentsBilling.BussinessLayer.Configuration.Validations.BillValidation
{
    public class CreateBillDtoValidation : AbstractValidator<CreateBillDto>
    {
        public CreateBillDtoValidation()
        {
            RuleFor(x => x.BilltypeId).NotNull().WithMessage("Fatura Tip Id si Boş Geçilemez");
            RuleFor(x => x.TotalPrice).NotNull().WithMessage("Fiyat Kısmı Boş Geçilemez");
            RuleFor(x => x.LastPaymentDate).NotNull().WithMessage("Sön Ödeme Tarhih Boş Geçilemez").
                Must(BeAValidDate).WithMessage("Tarih Kısmı Düzgün Giriniz ve Son Ödeme Tarihi Bügünden En az 5 Gün Sonrasına Olmak Zorundadır");
        }

        private bool BeAValidDate(DateTime date)
        {
            if (date < DateTime.Now.AddDays(5) && !date.Equals(default))
                return false;
            else
                return true;
        }
    }
}
