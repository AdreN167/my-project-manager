using MyProjectManager.Domain.Interfaces;

namespace MyProjectManager.Domain.Entity;

public class ProjectTask : IEntityId<long>, IAuditable
{
    public long Id { get; set; }
    public string Deadline { get; set; }
    public string Description { get; set; }
    public bool IsDone {  get; set; }
    public Project Project { get; set; }
    public long ProjectId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long? UpdatedBy { get; set; }

}
