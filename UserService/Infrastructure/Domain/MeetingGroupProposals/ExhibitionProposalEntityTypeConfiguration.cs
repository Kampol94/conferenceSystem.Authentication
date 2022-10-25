using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.ExhibitionProposals;
using UserService.Domain.Users;

namespace UserService.Infrastructure.Domain.MeetingGroupProposals;

public class ExhibitionProposalEntityTypeConfiguration : IEntityTypeConfiguration<ExhibitionProposal>
{
    public void Configure(EntityTypeBuilder<ExhibitionProposal> builder)
    {
        builder.ToTable("ExhibitionProposals", "management");

        builder.Property<ExhibitionProposalsId>("Id").HasConversion(v => v.Value, c => new ExhibitionProposalsId(c));
        builder.HasKey("Id");

        builder.Property<string>("_name").HasColumnName("Name");
        builder.Property<string>("_description").HasColumnName("Description");
        builder.Property<UserId>("_proposalUserId").HasColumnName("ProposalUserId").HasConversion(v => v.Value, c => new UserId(c));
        builder.Property<DateTime>("_proposalDate").HasColumnName("ProposalDate");

        builder.OwnsOne<ExhibitionProposalsStatus>("_status", b =>
        {
            b.Property(p => p.Value).HasColumnName("StatusCode");
        });

        builder.OwnsOne<ExhibitionProposalsDecision>("_decision", b =>
        {
            b.Property(p => p.Code).HasColumnName("DecisionCode");
            b.Property(p => p.Date).HasColumnName("DecisionDate");
            b.Property(p => p.RejectReason).HasColumnName("DecisionRejectReason");
            b.Property(p => p.UserId).HasColumnName("DecisionUserId").HasConversion(v => v.Value, c => new UserId(c));
        });
    }
}
