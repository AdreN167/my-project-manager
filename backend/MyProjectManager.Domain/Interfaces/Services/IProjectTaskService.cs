using MyProjectManager.Domain.Dto.ProjectTask;
using MyProjectManager.Domain.Result;

namespace MyProjectManager.Domain.Interfaces.Services;

public interface IProjectTaskService
{
    /// <summary>
    /// Полученеи всех задач из проекта
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<CollectionResult<ProjectTaskDto>> GetProjectTasksAsync(long projectId);

    /// <summary>
    /// Получение задачи по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<ProjectTaskDto>> GetProjectTaskByIdAsync(long id);

    /// <summary>
    /// Создание задачи с базовыми параметрами
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<ProjectTaskDto>> CreateProjectTaskAsync(CreateProjectTaskDto dto);

    /// <summary>
    /// Удаление задачи по id
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<ProjectTaskDto>> DeleteProjectTaskAsync(long id);

    /// <summary>
    /// Обновление задачи
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<ProjectTaskDto>> UpdateProjectTaskAsync(UpdateProjectTaskDto dto);
}
