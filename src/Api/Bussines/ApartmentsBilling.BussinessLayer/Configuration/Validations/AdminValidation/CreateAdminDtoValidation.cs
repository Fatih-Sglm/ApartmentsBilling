using ApartmentsBilling.Common.Dtos.AdminDto;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ApartmentsBilling.BussinessLayer.Configuration.Validations.AdminValidation
{
    public class CreateAdminDtoValidation : AbstractValidator<CreateAdminDto>
    {
        public CreateAdminDtoValidation()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Ad Kısmı Boş Geçilemez")
             .MaximumLength(15).WithMessage("İsminiz En fazla 15 karakterden oluşabilir"); ;
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-Mail Kısmı Boş Geçilemez")
                .EmailAddress().WithMessage("Lütfen Geçerli Bir E-mail Adresi Giriniz!");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Lütfen Telefon Numaranızı giriniz")
                .Length(10).WithMessage("Telefon Numarası 10 Haneli olmak zorunda ")
                .Matches(new Regex(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$"))
                .WithMessage("Telefon Numarası Geçersiz Lütfen Başında 0 Olmadan Giriniz");
            RuleFor(x => x.IdNumber).NotEmpty().WithMessage("TC No Boş Geçilemez").Length(11).WithMessage("TC NO 11 Haneli olmak Zorundadır.");
            RuleFor(x => x.WhichBlock).NotEmpty().WithMessage("Blok Adı Boş Geçilemez");
            RuleFor(x => x.FloorLocation).NotEmpty().WithMessage("Kat Numarası Boş Bırakılamaz");
            RuleFor(x => x.ApartmentName).NotEmpty().WithMessage("Apartman Adı Boş Bırakılamaz");
            RuleFor(x => x.FloorType).NotEmpty().WithMessage("Daire Tipi Boş Bırakılamaz");
            RuleFor(x => x.FloorNumber).NotEmpty().WithMessage("Daire Numarası Boş Bırakılamaz");
        }
    }
}
