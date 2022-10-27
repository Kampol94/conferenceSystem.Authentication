using MediatR;
using UserService.Application.Contracts.Commands;
using UserService.Domain.UserRegistrations;

namespace UserService.Application.UserRegistrations.ConfirmUserRegistration;

internal class ConfirmUserRegistrationCommandHandler : ICommandHandler<ConfirmUserRegistrationCommand>
{
    private readonly IUserRegistrationRepository _userRegistrationRepository;
    private readonly IMediator _mediator;

    public ConfirmUserRegistrationCommandHandler(IUserRegistrationRepository userRegistrationRepository, IMediator mediator)
    {
        _userRegistrationRepository = userRegistrationRepository;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(ConfirmUserRegistrationCommand request, CancellationToken cancellationToken)
    {
        var userRegistration =
            await _userRegistrationRepository.GetByIdAsync(new UserRegistrationId(request.UserRegistrationId));

        userRegistration.Confirm();
        
        return Unit.Value;
    }
}