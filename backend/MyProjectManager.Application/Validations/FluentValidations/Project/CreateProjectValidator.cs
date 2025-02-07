using FluentValidation;
using MyProjectManager.Domain.Dto.Project;

namespace MyProjectManager.Application.Validations.FluentValidations.Project;

public class CreateProjectValidator : AbstractValidator<CreateProjectDto>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.Color).NotEmpty().MaximumLength(10);
    }
}
