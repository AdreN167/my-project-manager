using MyProjectManager.Domain.Interfaces;

namespace MyProjectManager.Domain.Entity;

public class Project : IEntityId<long>, IAuditable
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
    public User User { get; set; }
    public string UserLogin { get; set; } // для создания связи в dbContext
    public List<ProjectTask> Tasks { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long? UpdatedBy { get; set; }
}
