using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProjectManager.Domain.Entity;

namespace MyProjectManager.DAL.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(2000);

        builder.HasMany<ProjectTask>(x => x.Tasks)
            .WithOne(x => x.Project)
            .HasForeignKey(x => x.ProjectId)
            .HasPrincipalKey(x => x.Id);

        //builder.HasData(new List<Project>()
        //{
        //    new Project()
        //    {
        //        Id = 1,
        //        Description = "Test description",
        //        Name = "Test #1",
        //        CreatedAt = DateTime.UtcNow,
        //        UserId = 1,
        //        Color = "#FFFFFF"
        //    }
        //});
    }
}
