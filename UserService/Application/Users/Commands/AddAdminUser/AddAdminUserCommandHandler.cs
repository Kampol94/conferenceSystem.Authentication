using MediatR;
using UserService.Application.Authentication;
using UserService.Application.Contracts.Commands;
using UserService.Domain.Users;

namespace UserService.Application.Users.AddAdminUser;

internal class AddAdminUserCommandHandler : ICommandHandler<AddAdminUserCommand>
{
    private readonly IUserRepository _userRepository;

    public AddAdminUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(AddAdminUserCommand command, CancellationToken cancellationToken)
    {
        var password = PasswordManager.HashPassword(command.Password);

        var user = User.CreateAdmin(
            command.Login,
            password,
            command.Email,
            command.FirstName,
            command.LastName,
            command.Name);

        await _userRepository.AddAsync(user);

        return Unit.Value;
    }
}