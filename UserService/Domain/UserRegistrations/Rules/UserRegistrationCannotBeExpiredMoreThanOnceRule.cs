using UserService.Domain.Contracts;
namespace UserService.Domain.UserRegistrations.Rules;

public class UserRegistrationCannotBeExpiredMoreThanOnceRule : IBaseBusinessRule
{
    private readonly UserRegistrationStatus _actualRegistrationStatus;

    internal UserRegistrationCannotBeExpiredMoreThanOnceRule(UserRegistrationStatus actualRegistrationStatus)
    {
        _actualRegistrationStatus = actualRegistrationStatus;
    }

    public bool IsBroken() => _actualRegistrationStatus == UserRegistrationStatus.Expired;

    public string Message => "User Registration cannot be expired more than once";
}