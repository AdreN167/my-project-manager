using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProjectManager.Domain.Entity;

namespace MyProjectManager.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Login).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Password).IsRequired();

        builder.HasMany<Project>(x => x.Projects)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserLogin)
            .HasPrincipalKey(x => x.Login);


        //builder.HasData(new List<User>()
        //{
        //    new User()
        //    {
        //        Id = 1,
        //        Login = "123",
        //        Password = "123",
        //        CreatedAt = DateTime.UtcNow,
        //    }
        //});
    }
}
