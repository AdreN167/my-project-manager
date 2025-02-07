using MyProjectManager.Domain.Result;

namespace MyProjectManager.Domain.Interfaces.Validations;

public interface IBaseValidator<in T> where T : class
{
    BaseResult ValidateOnNull(T model);
}
