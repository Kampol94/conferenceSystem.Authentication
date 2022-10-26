using UserService.Domain.Contracts;

namespace UserService.Domain.UserRegistrations;

public class UserRegistrationId : IdValueBase
{
    public UserRegistrationId(Guid value)
        : base(value)
    {
    }
}