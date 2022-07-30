using Finbuckle.MultiTenant.EntityFrameworkCore;
using AuthenticationService.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationService.Infrastructure.Persistence.Configuration;

public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .ToTable("Users", SchemaNames.Identity);

        builder
            .Property(u => u.ObjectId)
                .HasMaxLength(256);
    }
}

public class IdentityUserLoginConfig : IEntityTypeConfiguration<IdentityUserLogin<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder) =>
        builder
            .ToTable("UserLogins", SchemaNames.Identity)
            .IsMultiTenant();
}

public class IdentityUserTokenConfig : IEntityTypeConfiguration<IdentityUserToken<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder) =>
        builder
            .ToTable("UserTokens", SchemaNames.Identity)
            .IsMultiTenant();
}