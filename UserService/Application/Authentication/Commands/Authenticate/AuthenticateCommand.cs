﻿using UserService.Application.Contracts.Commands;

namespace UserService.Application.Authentication.Authenticate;

public class AuthenticateCommand : CommandBase<AuthenticationResult>
{
    public AuthenticateCommand(string login, string password)
    {
        Login = login;
        Password = password;
    }

    public string Login { get; }

    public string Password { get; }
}