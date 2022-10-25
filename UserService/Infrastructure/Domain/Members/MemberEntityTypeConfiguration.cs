using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Members;

namespace UserService.Infrastructure.Domain.Members;

public class MemberEntityTypeConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members", "management");

        builder.Property<MemberId>("Id").HasConversion(v => v.Value, c => new MemberId(c));
        builder.HasKey("Id");

        builder.Property<string>("_login").HasColumnName("Login");
        builder.Property<string>("_email").HasColumnName("Email");
        builder.Property<string>("_firstName").HasColumnName("FirstName");
        builder.Property<string>("_lastName").HasColumnName("LastName");
        builder.Property<string>("_name").HasColumnName("Name");
    }
}
