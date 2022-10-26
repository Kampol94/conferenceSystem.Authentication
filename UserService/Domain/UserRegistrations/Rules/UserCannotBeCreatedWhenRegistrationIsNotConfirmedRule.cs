using UserService.Domain.Contracts;

namespace UserService.Domain.UserRegistrations.Rules;

public class UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule : IBaseBusinessRule
{
    private readonly UserRegistrationStatus _actualRegistrationStatus;

    internal UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule(UserRegistrationStatus actualRegistrationStatus)
    {
        _actualRegistrationStatus = actualRegistrationStatus;
    }

    public bool IsBroken() => _actualRegistrationStatus != UserRegistrationStatus.Confirmed;

    public string Message => "User cannot be created when registration is not confirmed";
}