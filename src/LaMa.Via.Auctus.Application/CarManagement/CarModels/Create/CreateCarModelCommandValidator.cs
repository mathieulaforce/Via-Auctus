using FluentValidation;

namespace LaMa.Via.Auctus.Application.CarManagement.CarModels.Create;

internal class CreateCarModelCommandValidator : AbstractValidator<CreateCarModelCommand>
{
    public CreateCarModelCommandValidator()
    {
        RuleFor(cmd => cmd.Name).NotEmpty();
        RuleFor(cmd => cmd.CarBrandId).NotNull();
        RuleFor(cmd => cmd.CarBrandId.Value).NotEmpty();
    }
}