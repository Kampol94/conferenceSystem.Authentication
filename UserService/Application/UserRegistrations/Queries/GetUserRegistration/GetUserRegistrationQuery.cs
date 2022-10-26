using UserService.Application.Contracts.Queries;

namespace UserService.Application.UserRegistrations.GetUserRegistration;

public class GetUserRegistrationQuery : QueryBase<UserRegistrationDto>
{
    public GetUserRegistrationQuery(Guid userRegistrationId)
    {
        UserRegistrationId = userRegistrationId;
    }

    public Guid UserRegistrationId { get; }
}