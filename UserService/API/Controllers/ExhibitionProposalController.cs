using Microsoft.AspNetCore.Mvc;
using UserService.Application.ExhibitionProposals.GetExhibitionProposals;
using UserService.Application.ExhibitionProposals.AcceptExhibitionProposal;
using UserService.Application.ExhibitionProposals.GetExhibitionProposal;

namespace UserService.API.Controllers;

public class ExhibitionProposalController : BaseApiController
{
    [HttpGet("")]
    [ProducesResponseType(typeof(List<ExhibitionProposalDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetExhibitionProposals()
    {
        return Ok(await Mediator.Send(new GetExhibitionProposalssQuery()));
    }

    [HttpPatch("{meetingGroupProposalId}/accept")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AcceptExhibitionProposal(Guid meetingGroupProposalId)
    {
        return Ok(await Mediator.Send(new AcceptExhibitionProposalsCommand(meetingGroupProposalId)));
    }
}
