using FluentValidation;

namespace LaMa.Via.Auctus.Application.CarManagement.Cars.Create;
 
internal class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
{
    public CreateCarCommandValidator()
    {
        RuleFor(cmd => cmd.CarBrandId).NotNull();
        RuleFor(cmd => cmd.CarBrandId.Value).NotEmpty();
        RuleFor(cmd => cmd.CarModelId).NotNull();
        RuleFor(cmd => cmd.CarModelId.Value).NotEmpty();
        RuleFor(cmd => cmd.CarModelVersionId).NotNull();
        RuleFor(cmd => cmd.CarModelVersionId.Value).NotEmpty(); 
        RuleFor(cmd => cmd.EngineId).NotNull();
        RuleFor(cmd => cmd.EngineId.Value).NotEmpty();
    }
}