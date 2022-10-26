using UserService.Domain.Contracts;
using UserService.Domain.UserRegistrations;
using UserService.Domain.Users.Events;

namespace UserService.Domain.Users;

public class User : BaseEntity
{
    public UserId Id { get; private set; }

    private string _login;

    private string _password;

    private string _email;

    private bool _isActive;

    private string _firstName;

    private string _lastName;

    private string _name;

    private List<UserRole> _roles;

    private User()
    {
        // Only for EF.
    }

    public static User CreateAdmin(
        string login,
        string password,
        string email,
        string firstName,
        string lastName,
        string name)
    {
        return new User(
            Guid.NewGuid(),
            login,
            password,
            email,
            firstName,
            lastName,
            name,
            UserRole.Administrator);
    }

    internal static User CreateFromUserRegistration(
        UserRegistrationId userRegistrationId,
        string login,
        string password,
        string email,
        string firstName,
        string lastName,
        string name)
    {
        return new User(
            userRegistrationId.Value,
            login,
            password,
            email,
            firstName,
            lastName,
            name,
            UserRole.Member);
    }

    private User(
        Guid id,
        string login,
        string password,
        string email,
        string firstName,
        string lastName,
        string name,
        UserRole role)
    {
        Id = new UserId(id);
        _login = login;
        _password = password;
        _email = email;
        _firstName = firstName;
        _lastName = lastName;
        _name = name;

        _isActive = true;

        _roles = new List<UserRole>();
        _roles.Add(role);

        this.AddDomainEvent(new UserCreatedDomainEvent(Id));
    }
}