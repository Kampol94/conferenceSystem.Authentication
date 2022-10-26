using UserService.Domain.Contracts;
namespace UserService.Domain.UserRegistrations.Rules;

public class UserRegistrationCannotBeConfirmedMoreThanOnceRule : IBaseBusinessRule
{
    private readonly UserRegistrationStatus _actualRegistrationStatus;

    internal UserRegistrationCannotBeConfirmedMoreThanOnceRule(UserRegistrationStatus actualRegistrationStatus)
    {
        _actualRegistrationStatus = actualRegistrationStatus;
    }

    public bool IsBroken() => _actualRegistrationStatus == UserRegistrationStatus.Confirmed;

    public string Message => "User Registration cannot be confirmed more than once";
}