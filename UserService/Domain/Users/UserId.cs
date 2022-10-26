using UserService.Domain.Contracts;
namespace UserService.Domain.Users;

public class UserId : IdValueBase
{
    public UserId(Guid value)
        : base(value)
    {
    }
}