using ApartmentsBilling.Common.Dtos.UserDtos;
using FluentValidation;

namespace ApartmentsBilling.BussinessLayer.Configuration.Validations.UserValidation
{
    public class LoginUserDtoValidation : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-Mail Kısmı Boş Geçilemez")
                .EmailAddress().WithMessage("Lütfen Geçerli Bir E-mail Adresi Giriniz!");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre Kısmı Boş Geçilemez");
        }
    }
}
