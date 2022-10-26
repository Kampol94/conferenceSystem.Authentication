using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using UserService.Application.Authentication.Commands.Authenticate;
using UserService.Application.Contracts;
using UserService.Application.Contracts.Commands;
using UserService.Domain.Users;

namespace UserService.Application.Authentication.Authenticate;

public class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand, AuthenticationResult>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public AuthenticateCommandHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<AuthenticationResult> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sql = "SELECT " +
                           "[User].[Id], " +
                           "[User].[Login], " +
                           "[User].[Name], " +
                           "[User].[Email], " +
                           "[User].[IsActive], " +
                           "[User].[Password] " +
                           "FROM [users].[Users] AS [User] " +
                           "WHERE [User].[Login] = @Login";

        var user = await connection.QuerySingleOrDefaultAsync<User>(
            sql,
            new
            {
                request.Login,
            });

        if (user == null)
        {
            return new AuthenticationResult("Incorrect login or password");
        }

        if (!user.IsActive)
        {
            return new AuthenticationResult("User is not active");
        }

        if (!PasswordManager.VerifyHashedPassword(user.Password, request.Password))
        {
            return new AuthenticationResult("Incorrect login or password");
        }

        var token = GenerateJwtToken(user);

        return new AuthenticationResult(token);
    }

    private static Token GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("BigSecret"); //TODO: change to appSetting 
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim(MyClaimTypes.Name, user.Name),
                new Claim(MyClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return  new Token(tokenHandler.WriteToken(token));
    }
}