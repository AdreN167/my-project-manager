using MyProjectManager.Domain.Entity;
using MyProjectManager.Domain.Result;

namespace MyProjectManager.Domain.Interfaces.Validations;

public interface IProjectTaskValidator : IBaseValidator<ProjectTask>
{
    BaseResult CreateValidator(Project project);
}
