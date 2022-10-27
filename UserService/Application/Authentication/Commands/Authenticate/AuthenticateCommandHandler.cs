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
    private readonly IUserRepository _userRepository;

    public AuthenticateCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<AuthenticationResult> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetAllAsync().Where(x => x.Login == request.Login).FirstOrDefault();

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

        var claims = new List<Claim>
        {
            new Claim(MyClaimTypes.Id, user.Id.Value.ToString()),
            new Claim(MyClaimTypes.Name, user.Name),
            new Claim(MyClaimTypes.Email, user.Email)
        };

        var token = GenerateJwtToken(claims);

        return new AuthenticationResult(token);
    }

    private static Token GenerateJwtToken(List<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("this is my custom Secret key for authentication"); //TODO: change to appSetting 
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return  new Token(tokenHandler.WriteToken(token));
    }
}