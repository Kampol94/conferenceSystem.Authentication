using UserService.Domain.Contracts;
using UserService.Domain.UserRegistrations;
using UserService.Domain.Users.Events;

namespace UserService.Domain.Users;

public class User : BaseEntity
{
    public UserId Id { get; private set; }

    public string Email { get; private set; }

    public string Password { get; private set; }

    public bool IsActive { get; private set; }

    private string _firstName;

    private string _lastName;

    public string Name { get; private set; }
    public string Login { get; private set; }

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
        Login = login;
        Password = password;
        Email = email;
        _firstName = firstName;
        _lastName = lastName;
        Name = name;

        IsActive = true;

        _roles = new List<UserRole>();
        _roles.Add(role);

        this.AddDomainEvent(new UserCreatedDomainEvent(Id));
    }
}