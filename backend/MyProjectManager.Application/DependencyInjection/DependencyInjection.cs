using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MyProjectManager.Application.Mapping;
using MyProjectManager.Application.Services;
using MyProjectManager.Application.Validations;
using MyProjectManager.Application.Validations.FluentValidations.Project;
using MyProjectManager.Application.Validations.FluentValidations.ProjectTask;
using MyProjectManager.Domain.Dto.Project;
using MyProjectManager.Domain.Dto.ProjectTask;
using MyProjectManager.Domain.Interfaces.Services;
using MyProjectManager.Domain.Interfaces.Validations;

namespace MyProjectManager.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ProjectMapping));

        InitServices(services);
    }

    private static void InitServices(this IServiceCollection services)
    {
        // регаем валидаторы
        services.AddScoped<IProjectValidator, ProjectValidator>();
        services.AddScoped<IValidator<CreateProjectDto>, CreateProjectValidator>();
        services.AddScoped<IValidator<UpdateProjectDto>, UpdateProjectValidator>();
        services.AddScoped<IProjectTaskValidator, ProjectTaskValidator>();
        services.AddScoped<IValidator<CreateProjectTaskDto>, CreateProjectTaskValidator>();
        services.AddScoped<IValidator<UpdateProjectTaskDto>, UpdateProjectTaskValidator>();

        // регаем сервисы
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IProjectTaskService, ProjectTaskService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
    }
}
