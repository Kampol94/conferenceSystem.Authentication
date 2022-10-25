using Microsoft.EntityFrameworkCore;
using UserService.Domain.ExhibitionProposals;
using UserService.Infrastructure;

namespace UserService.Infrastructure.Domain.MeetingGroupProposals;

public class ExhibitionProposalRepository : IExhibitionProposalsRepository
{
    private readonly ManagementContext _context;

    public ExhibitionProposalRepository(ManagementContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ExhibitionProposal exhibitionProposal)
    {
        await _context.ExhibitionProposals.AddAsync(exhibitionProposal);
    }

    public async Task<ExhibitionProposal> GetByIdAsync(ExhibitionProposalsId exhibitionProposalId)
    {
        return await _context.ExhibitionProposals.FirstOrDefaultAsync(x => x.Id == exhibitionProposalId);
    }
}
