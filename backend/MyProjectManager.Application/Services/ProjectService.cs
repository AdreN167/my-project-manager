using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyProjectManager.Application.Resources;
using MyProjectManager.Domain.Dto.Project;
using MyProjectManager.Domain.Entity;
using MyProjectManager.Domain.Enum;
using MyProjectManager.Domain.Interfaces.Repositories;
using MyProjectManager.Domain.Interfaces.Services;
using MyProjectManager.Domain.Interfaces.Validations;
using MyProjectManager.Domain.Result;
using Serilog;

namespace MyProjectManager.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IBaseRepository<Project> _projectRepository;
    private readonly IBaseRepository<ProjectTask> _projectTaskRepository;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IProjectValidator _projectValidator;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public ProjectService(
        IBaseRepository<Project> projectRepository, 
        IBaseRepository<User> userRepository, 
        IProjectValidator projectValidator, 
        ILogger logger, 
        IMapper mapper
    )
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
        _projectValidator = projectValidator;
        _logger = logger;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<BaseResult<ProjectDto>> CreateProjectAsync(CreateProjectDto dto)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == dto.UserLogin);
            var project = await _projectRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);
            var result = _projectValidator.CreateValidator(project, user);

            if (!result.IsSuccess) 
            {
                return new BaseResult<ProjectDto>()
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode
                };  
            }

            project = new Project()
            {
                Name = dto.Name,
                Description = dto.Description,
                UserLogin = user.Login,
                Color = dto.Color,
            };

            await _projectRepository.CreateAsync(project);

            return new BaseResult<ProjectDto>()
            {
                Data = _mapper.Map<ProjectDto>(project)
            };
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new BaseResult<ProjectDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }
    }

    /// <inheritdoc/>
    public async Task<BaseResult<ProjectDto>> DeleteProjectAsync(long id)
    {
        
        try
        {
            
            var project = await _projectRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            var result = _projectValidator.ValidateOnNull(project);

            if (!result.IsSuccess)
            {
                return new BaseResult<ProjectDto>()
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode
                };
            }

            await _projectRepository.RemoveAsync(project);

            return new BaseResult<ProjectDto>()
            {
                Data = _mapper.Map<ProjectDto>(project)
            };
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new BaseResult<ProjectDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }
    }

    /// <inheritdoc/>
    public Task<BaseResult<ProjectDto>> GetProjectByIdAsync(long id)
    {
        ProjectDto? project;
        try
        {
            project = _projectRepository.GetAll()
                .AsEnumerable()
                .Select(x => new ProjectDto(x.Id, x.Name, x.Color, x.Description, x.CreatedAt.ToLongDateString()))
                .FirstOrDefault(x => x.Id == id);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return Task.FromResult(new BaseResult<ProjectDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            });
        }

        // если не найден проект
        if (project == null)
        {
            _logger.Warning($"Проект с id = {id} не найден", id);
            return Task.FromResult(new BaseResult<ProjectDto>()
            {
                ErrorMessage = ErrorMessage.ProjectNotFound,
                ErrorCode = (int)ErrorCodes.ProjectNotFound
            });
        }

        return Task.FromResult(new BaseResult<ProjectDto>()
        {
            Data = project
        });
    }

    /// <inheritdoc/>
    public async Task<CollectionResult<ProjectDto>> GetProjectsAsync(string userLogin)
    {
        ProjectDto[] projects;

        try
        {
            // ToArrayAsync тянется из EF Core!!!
            projects = await _projectRepository.GetAll()
                .Where(x => x.UserLogin == userLogin)
                .Select(x => new ProjectDto(x.Id, x.Name, x.Color, x.Description, x.CreatedAt.ToLongDateString()))
                .ToArrayAsync();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new CollectionResult<ProjectDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }

        // если не найдено проектов
        if (!projects.Any())
        {
            _logger.Warning(ErrorMessage.ProjectsNotFound, projects.Length);
            return new CollectionResult<ProjectDto>()
            {
                ErrorMessage = ErrorMessage.ProjectsNotFound,
                ErrorCode = (int)ErrorCodes.ProjectsNotFound
            };
        }

        return new CollectionResult<ProjectDto>()
        {
            Data = projects,
            Count = projects.Length
        };
    }

    public async Task<BaseResult<ProjectDto>> UpdateProjectAsync(UpdateProjectDto dto)
    {
        try
        {
            var project = await _projectRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);
            var result = _projectValidator.ValidateOnNull(project);

            if (!result.IsSuccess)
            {
                return new BaseResult<ProjectDto>()
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode
                };
            }

            project.Name = dto.Name;
            project.Description = dto.Description;
            project.Color = dto.Color;
    
            await _projectRepository.UpdateAsync(project);

            return new BaseResult<ProjectDto>()
            {
                Data = _mapper.Map<ProjectDto>(project)
            };
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new BaseResult<ProjectDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }
    }
}
