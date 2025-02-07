using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyProjectManager.Application.Resources;
using MyProjectManager.Domain.Dto.ProjectTask;
using MyProjectManager.Domain.Entity;
using MyProjectManager.Domain.Enum;
using MyProjectManager.Domain.Interfaces.Repositories;
using MyProjectManager.Domain.Interfaces.Services;
using MyProjectManager.Domain.Interfaces.Validations;
using MyProjectManager.Domain.Result;
using Serilog;

namespace MyProjectManager.Application.Services;

public class ProjectTaskService : IProjectTaskService
{
    private readonly IBaseRepository<ProjectTask> _projectTaskRepository;
    private readonly IBaseRepository<Project> _projectRepository;
    private readonly IProjectTaskValidator _projectTaskValidator;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public ProjectTaskService(
        IBaseRepository<ProjectTask> projectTaskRepository,
        IBaseRepository<Project> projectRepository,
        IProjectTaskValidator projectTaskValidator,
        ILogger logger,
        IMapper mapper
    )
    {
        _projectTaskRepository = projectTaskRepository;
        _projectRepository = projectRepository;
        _projectTaskValidator = projectTaskValidator;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<BaseResult<ProjectTaskDto>> CreateProjectTaskAsync(CreateProjectTaskDto dto)
    {
        try
        {
            var project = await _projectRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.ProjectId);
            var result = _projectTaskValidator.CreateValidator(project);

            if (!result.IsSuccess)
            {
                return new BaseResult<ProjectTaskDto>()
                {
                    ErrorCode = result.ErrorCode,
                    ErrorMessage = result.ErrorMessage
                };
            }

            var task = new ProjectTask()
            {
                Deadline = dto.Deadline,
                ProjectId = dto.ProjectId,
                Description = dto.Description,
                IsDone = false
            };

            await _projectTaskRepository.CreateAsync(task);

            return new BaseResult<ProjectTaskDto>()
            {
                Data = _mapper.Map<ProjectTaskDto>(task),
            };
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new BaseResult<ProjectTaskDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }
    }

    public async Task<BaseResult<ProjectTaskDto>> DeleteProjectTaskAsync(long id)
    {
        try
        {
            var task = await _projectTaskRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            var result = _projectTaskValidator.ValidateOnNull(task);

            if (!result.IsSuccess)
            {
                return new BaseResult<ProjectTaskDto>()
                {
                    ErrorCode = result.ErrorCode,
                    ErrorMessage = result.ErrorMessage
                };
            }

            await _projectTaskRepository.RemoveAsync(task);

            return new BaseResult<ProjectTaskDto>()
            {
                Data = _mapper.Map<ProjectTaskDto>(task)
            };
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new BaseResult<ProjectTaskDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }
    }

    public Task<BaseResult<ProjectTaskDto>> GetProjectTaskByIdAsync(long id)
    {
        ProjectTaskDto? task;
        try
        {
            task = _projectTaskRepository.GetAll()
                .AsEnumerable()
                .Select(x => new ProjectTaskDto(x.Id, x.Deadline, x.Description, x.IsDone, x.CreatedAt.ToLongDateString()))
                .FirstOrDefault(x => x.Id == id);

        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return Task.FromResult(new BaseResult<ProjectTaskDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            });
        }

        if (task == null)
        {
            _logger.Warning($"Задача с id = {id} не найдена", id);
            return Task.FromResult(new BaseResult<ProjectTaskDto>()
            {
                ErrorMessage = ErrorMessage.TaskNotFound,
                ErrorCode = (int)ErrorCodes.TaskNotFound
            });
        }

        return Task.FromResult(new BaseResult<ProjectTaskDto>()
        {
            Data = task
        });
    }

    public async Task<CollectionResult<ProjectTaskDto>> GetProjectTasksAsync(long projectId)
    {
        ProjectTaskDto[] tasks;
        try
        {
            tasks = await _projectTaskRepository.GetAll()
                .Where(x => x.ProjectId == projectId)
                .Select(x => new ProjectTaskDto(x.Id, x.Deadline, x.Description, x.IsDone, x.CreatedAt.ToLongDateString()))
                .ToArrayAsync();

        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new CollectionResult<ProjectTaskDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }

        if (tasks == null || tasks.Length == 0)
        {
            _logger.Warning(ErrorMessage.TaskNotFound, tasks.Length);
            return new CollectionResult<ProjectTaskDto>()
            {
                ErrorMessage = ErrorMessage.TaskNotFound,
                ErrorCode = (int)ErrorCodes.TaskNotFound
            };
        }

        return new CollectionResult<ProjectTaskDto>()
        {
            Data = tasks,
            Count = tasks.Length
        };
    }

    public async Task<BaseResult<ProjectTaskDto>> UpdateProjectTaskAsync(UpdateProjectTaskDto dto)
    {
        try
        {
            var task = await _projectTaskRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);
            var result = _projectTaskValidator.ValidateOnNull(task);

            if (!result.IsSuccess)
            {
                return new BaseResult<ProjectTaskDto>()
                {
                    ErrorCode = result.ErrorCode,
                    ErrorMessage = result.ErrorMessage
                };
            }

            task.Deadline = dto.Deadline;
            task.Description = dto.Description;
            task.IsDone = dto.IsDone;

            await _projectTaskRepository.UpdateAsync(task);

            return new BaseResult<ProjectTaskDto>()
            {
                Data = _mapper.Map<ProjectTaskDto>(task)
            };
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new BaseResult<ProjectTaskDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }
    }
}
