using ApartmentsBilling.Common.Dtos.UserDtos;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ApartmentsBilling.BussinessLayer.Configuration.Validations.UserValidation
{
    public class UpdateUserDtoValidation : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id Boş Geçilemez");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Ad Kısmı Boş Geçilemez")
             .MaximumLength(15).WithMessage("İsminiz En fazla 15 karakterden oluşabilir"); ;
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-Mail Kısmı Boş Geçilemez")
                .EmailAddress().WithMessage("Lütfen Geçerli Bir E-mail Adresi Giriniz!");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Lütfen Telefon Numaranızı giriniz")
                .Length(10).WithMessage("Telefon Numarası 10 Haneli olmak zorunda ")
                .Matches(new Regex(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$"))
                .WithMessage("Telefon Numarası Geçersiz Lütfen Başında 0 Olmadan Giriniz");
            RuleFor(x => x.IdNumber).NotEmpty().WithMessage("TC No Boş Geçilemez").Length(11).WithMessage("TC NO 11 Haneli olmak Zorundadır.");
        }
    }
}
