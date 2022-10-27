using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.UserRegistrations;
using UserService.Domain.Users;

namespace UserService.Infrastructure.Domain.Users;

internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "users");

        builder.Property<UserId>("Id").HasConversion(v => v.Value, c => new UserId(c));
        builder.HasKey("Id");

        builder.Property(x => x.Login).HasColumnName("Login");
        builder.Property(x => x.Email).HasColumnName("Email");
        builder.Property(x => x.Password).HasColumnName("Password");
        builder.Property(x => x.IsActive).HasColumnName("IsActive");
        builder.Property<string>("_firstName").HasColumnName("FirstName");
        builder.Property<string>("_lastName").HasColumnName("LastName");
        builder.Property(x => x.Name).HasColumnName("Name");

        builder.OwnsMany<UserRole>("_roles", b =>
        {
            b.WithOwner().HasForeignKey("UserId");
            b.ToTable("UserRoles", "users");
            b.Property<UserId>("UserId");
            b.Property<string>("Value").HasColumnName("RoleCode");
            b.HasKey("UserId", "Value");
        });
    }
}
