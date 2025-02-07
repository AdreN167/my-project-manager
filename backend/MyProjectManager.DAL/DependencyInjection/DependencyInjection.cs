using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyProjectManager.DAL.Interceptors;
using MyProjectManager.DAL.Repositories;
using MyProjectManager.Domain.Entity;
using MyProjectManager.Domain.Interfaces.Repositories;

namespace MyProjectManager.DAL.DependencyInjection;

public static class DependencyInjection
{
    // метод для подключения зависимостей из DAL
    public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresSQL");

        services.AddSingleton<DateInterceptor>();
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        services.InitRepositories();
    }

    // метод для регистрации репозиториев
    private static void InitRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
        services.AddScoped<IBaseRepository<UserToken>, BaseRepository<UserToken>>();
        services.AddScoped<IBaseRepository<Project>, BaseRepository<Project>>();
        services.AddScoped<IBaseRepository<ProjectTask>, BaseRepository<ProjectTask>>();
    }
}
