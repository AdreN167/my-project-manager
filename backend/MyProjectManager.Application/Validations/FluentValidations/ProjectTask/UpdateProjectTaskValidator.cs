using FluentValidation;
using MyProjectManager.Domain.Dto.ProjectTask;

namespace MyProjectManager.Application.Validations.FluentValidations.ProjectTask;

public class UpdateProjectTaskValidator : AbstractValidator<UpdateProjectTaskDto>
{
    public UpdateProjectTaskValidator()
    {
        RuleFor(x => x.Deadline).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.IsDone).NotEmpty();
    }
}
