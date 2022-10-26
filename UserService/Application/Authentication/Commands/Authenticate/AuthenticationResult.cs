using UserService.Application.Authentication.Commands.Authenticate;

namespace UserService.Application.Authentication.Authenticate;

public class AuthenticationResult
{
    public AuthenticationResult(string authenticationError)
    {
        IsAuthenticated = false;
        AuthenticationError = authenticationError;
    }

    public AuthenticationResult(Token token)
    {
        IsAuthenticated = true;
        Token = token;
    }

    public bool IsAuthenticated { get; }

    public string? AuthenticationError { get; }

    public Token? Token { get; }
}