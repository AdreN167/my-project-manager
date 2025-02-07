using MyProjectManager.Domain.Entity;
using MyProjectManager.Domain.Result;

namespace MyProjectManager.Domain.Interfaces.Validations;

public interface IProjectValidator : IBaseValidator<Project>
{
    /// <summary>
    /// Проверяется наличие проекта, если проект с переданным названием есть в БД, то создать проект с таким же названием нельзя
    /// Проверяется пользователь, если пользователя с переданным id нет в БД, то такого пользователя нет, нельзя создать проект
    /// </summary>
    /// <param name="project"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    BaseResult CreateValidator(Project project, User user);
}
