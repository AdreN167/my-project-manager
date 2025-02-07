using FluentValidation;
using MyProjectManager.Domain.Dto.ProjectTask;

namespace MyProjectManager.Application.Validations.FluentValidations.ProjectTask;

public class CreateProjectTaskValidator : AbstractValidator<CreateProjectTaskDto>
{
    public CreateProjectTaskValidator()
    {
        RuleFor(x => x.Deadline).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
    }
}
