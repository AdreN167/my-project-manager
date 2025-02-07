using MyProjectManager.Application.Resources;
using MyProjectManager.Domain.Entity;
using MyProjectManager.Domain.Enum;
using MyProjectManager.Domain.Interfaces.Validations;
using MyProjectManager.Domain.Result;

namespace MyProjectManager.Application.Validations;

public class ProjectValidator : IProjectValidator
{
    public BaseResult CreateValidator(Project project, User user)
    {
        if (project != null)
            return new BaseResult()
            {
                ErrorCode = (int)ErrorCodes.ProjectAlreadyExists,
                ErrorMessage = ErrorMessage.ProjectAlreadyExists
            };

        if (user == null)
            return new BaseResult()
            {
                ErrorCode = (int)ErrorCodes.UserNotFound,
                ErrorMessage = ErrorMessage.UserNotFound
            };

        return new BaseResult();
    }

    public BaseResult ValidateOnNull(Project model)
    {
        if (model == null)
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.ProjectNotFound,
                ErrorCode = (int)ErrorCodes.ProjectNotFound
            };

        return new BaseResult();
    }
}
