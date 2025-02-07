using MyProjectManager.Application.Resources;
using MyProjectManager.Domain.Entity;
using MyProjectManager.Domain.Enum;
using MyProjectManager.Domain.Interfaces.Validations;
using MyProjectManager.Domain.Result;

namespace MyProjectManager.Application.Validations;

public class ProjectTaskValidator : IProjectTaskValidator
{
    public BaseResult CreateValidator(Project project)
    {
        if (project == null)
            return new BaseResult()
            {
                ErrorCode = (int)ErrorCodes.ProjectNotFound,
                ErrorMessage = ErrorMessage.ProjectNotFound
            };

        return new BaseResult();
    }

    public BaseResult ValidateOnNull(ProjectTask model)
    {
        if (model == null)
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.TaskNotFound,
                ErrorCode = (int)ErrorCodes.TaskNotFound
            };

        return new BaseResult();
    }
}
