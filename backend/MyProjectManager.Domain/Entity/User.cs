using MyProjectManager.Domain.Interfaces;

namespace MyProjectManager.Domain.Entity;

public class User : IEntityId<long>, IAuditable
{
    public long Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public List<Project> Projects { get; set; }
    public UserToken UserToken { get; set; }
    public DateTime CreatedAt {  get; set; }
    public long CreatedBy {  get; set; }
    public DateTime? UpdatedAt {  get; set; }
    public long? UpdatedBy {  get; set; }
}
