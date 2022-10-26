using UserService.Domain.Contracts;
namespace UserService.Domain.UserRegistrations.Rules;

public class UserRegistrationCannotBeConfirmedAfterExpirationRule : IBaseBusinessRule
{
    private readonly UserRegistrationStatus _actualRegistrationStatus;

    internal UserRegistrationCannotBeConfirmedAfterExpirationRule(UserRegistrationStatus actualRegistrationStatus)
    {
        _actualRegistrationStatus = actualRegistrationStatus;
    }

    public bool IsBroken() => _actualRegistrationStatus == UserRegistrationStatus.Expired;

    public string Message => "User Registration cannot be confirmed because it is expired";
}