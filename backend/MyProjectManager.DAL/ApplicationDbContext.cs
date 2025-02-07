using Microsoft.EntityFrameworkCore;
using MyProjectManager.DAL.Interceptors;
using MyProjectManager.Domain.Entity;
using System.Reflection;

namespace MyProjectManager.DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new DateInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ApplyConfigurationsFromAssembly сразу подрубает все конфигурации из сборки автоматом
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
