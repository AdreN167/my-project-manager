using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MyProjectManager.Domain.Interfaces;

namespace MyProjectManager.DAL.Interceptors;

// нужен для перехвата запросов и редактирования этих самых запросов
// например, чтобы руками не заполнять данные аудита (когда создан, когда обновлен)
public class DateInterceptor : SaveChangesInterceptor
{
    // асинхронная версия
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext == null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var entries = dbContext.ChangeTracker.Entries<IAuditable>()
            .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified)
            .ToList();
        foreach (var entry in entries)
        {
            // если сущность бала добавлена
            if (entry.State == EntityState.Added)
            {
                entry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
            }

            // если сущность была изменена
            if (entry.State == EntityState.Modified)
            {
                entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
            }
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var dbContext = eventData.Context;
        if (dbContext == null)
        {
            return base.SavingChanges(eventData, result);
        }

        var entries = dbContext.ChangeTracker.Entries<IAuditable>()
            .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified)
            .ToList();
        foreach (var entry in entries)
        {
            // если сущность бала добавлена
            if (entry.State == EntityState.Added)
            {
                entry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
            }

            // если сущность была изменена
            if (entry.State == EntityState.Modified)
            {
                entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
            }
        }

        return base.SavingChanges(eventData, result);
    }
}

