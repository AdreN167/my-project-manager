using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyProjectManager.Domain.Entity;

namespace MyProjectManager.DAL.Configurations;

public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
{
    public void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Deadline).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(2000);

        //builder.HasData(new List<ProjectTask>()
        //{
        //    new ProjectTask()
        //    {
        //        Id = 1,
        //        Description = "Test task description",
        //        Name = "Test task #1",
        //        CreatedAt = DateTime.UtcNow,
        //        ProjectId = 1
        //    }
        //});
    }
}
