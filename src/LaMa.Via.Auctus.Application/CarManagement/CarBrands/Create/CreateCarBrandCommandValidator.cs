using FluentValidation;

namespace LaMa.Via.Auctus.Application.CarManagement.CarBrands.Create;

public class CreateCarBrandCommandValidator : AbstractValidator<CreateCarBrandCommand>
{
    public CreateCarBrandCommandValidator()
    {
        RuleFor(cmd => cmd.Name).NotEmpty();
        RuleFor(cmd => cmd.PrimaryColor).NotEmpty();
        RuleFor(cmd => cmd.FontFamily).NotEmpty();
        RuleFor(cmd => cmd.LogoSvgUrl).NotEmpty();
    }
}