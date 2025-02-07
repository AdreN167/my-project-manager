using MyProjectManager.Domain.Dto.Project;
using MyProjectManager.Domain.Result;

namespace MyProjectManager.Domain.Interfaces.Services;

/// <summary>
/// Сервис, отвечающий за работу с доменной частью проекта (Project)
/// </summary>
public interface IProjectService
{
    /// <summary>
    /// Полученеи всех проектов
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<CollectionResult<ProjectDto>> GetProjectsAsync(string userLogin);

    /// <summary>
    /// Получение отчета по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<ProjectDto>> GetProjectByIdAsync(long id);

    /// <summary>
    /// Создание проекта с базовыми параметрами
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<ProjectDto>> CreateProjectAsync(CreateProjectDto dto);

    /// <summary>
    /// Удаление проекта по id
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<ProjectDto>> DeleteProjectAsync(long id);

    /// <summary>
    /// Обновление проекта
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<ProjectDto>> UpdateProjectAsync(UpdateProjectDto dto);
}
