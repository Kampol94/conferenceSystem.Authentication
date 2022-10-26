using UserService.Application.Contracts.Queries;

namespace UserService.Application.Users.GetUser;

public class GetUserQuery : QueryBase<UserDto>
{
    public GetUserQuery(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}