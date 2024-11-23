using FluentValidation;

namespace LaMa.Via.Auctus.Application.CarManagement.CarBrands.Update;

internal class UpdateCarBrandCommandValidator : AbstractValidator<UpdateCarBrandCommand>
{
    public UpdateCarBrandCommandValidator()
    {
        RuleFor(cmd => cmd.Id.Value).NotEmpty();
        RuleFor(cmd => cmd.Name).NotEmpty();
        RuleFor(cmd => cmd.PrimaryColor).NotEmpty();
        RuleFor(cmd => cmd.FontFamily).NotEmpty();
        RuleFor(cmd => cmd.LogoSvgUrl).NotEmpty();
    }
}