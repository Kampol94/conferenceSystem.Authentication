﻿using UserService.Application.Authentication;
using UserService.Application.Contracts.Commands;
using UserService.Domain.UserRegistrations;

namespace UserService.Application.UserRegistrations.RegisterNewUser;

internal class RegisterNewUserCommandHandler : ICommandHandler<RegisterNewUserCommand, Guid>
{
    private readonly IUserRegistrationRepository _userRegistrationRepository;
    private readonly IUsersCounter _usersCounter;

    public RegisterNewUserCommandHandler(
        IUserRegistrationRepository userRegistrationRepository,
        IUsersCounter usersCounter)
    {
        _userRegistrationRepository = userRegistrationRepository;
        _usersCounter = usersCounter;
    }

    public async Task<Guid> Handle(RegisterNewUserCommand command, CancellationToken cancellationToken)
    {
        var password = PasswordManager.HashPassword(command.Password);

        var userRegistration = UserRegistration.RegisterNewUser(
            command.Login,
            password,
            command.Email,
            command.FirstName,
            command.LastName,
            _usersCounter,
            command.ConfirmLink);

        await _userRegistrationRepository.AddAsync(userRegistration);

        return userRegistration.Id.Value;
    }
}