using UserService.Application.Contracts.Queries;
using UserService.Application.Users.GetUser;

namespace UserService.Application.Users.GetAuthenticatedUser;

public class GetAuthenticatedUserQuery : QueryBase<UserDto>
{
    public GetAuthenticatedUserQuery()
    {
    }
}