using ApartmentsBilling.Common.Dtos.FlatDto;
using FluentValidation;

namespace ApartmentsBilling.BussinessLayer.Configuration.Validations.FlatValidation
{
    public class CreateFlatDtoValidation : AbstractValidator<CreateFlatDto>
    {
        public CreateFlatDtoValidation()
        {
            RuleFor(x => x.WhichBlock).NotEmpty().WithMessage("Blok Adı Boş Geçilemez");
            RuleFor(x => x.FloorLocation).NotEmpty().WithMessage("Kat Numarası Boş Bırakılamaz");
            RuleFor(x => x.FloorType).NotEmpty().WithMessage("Daire Tipi Boş Bırakılamaz");
            RuleFor(x => x.FloorNumber).NotEmpty().WithMessage("Daire Numarası Boş Bırakılamaz");
        }
    }
}
