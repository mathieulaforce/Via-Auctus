using FluentValidation;

namespace LaMa.Via.Auctus.Application.CarManagement.CarBrands.Update;

internal class CreateCarBrandCommandValidator : AbstractValidator<UpdateCarBrandCommand>
{
    public CreateCarBrandCommandValidator()
    {
        RuleFor(cmd => cmd.Id).NotEmpty();
        RuleFor(cmd => cmd.Name).NotEmpty();
        RuleFor(cmd => cmd.PrimaryColor).NotEmpty();
        RuleFor(cmd => cmd.FontFamily).NotEmpty();
        RuleFor(cmd => cmd.LogoSvgUrl).NotEmpty();
    }
}