using UserService.Domain.Users;

namespace UserService.Infrastructure.Domain.Users;

public class UserRepository : IUserRepository
{
    private readonly UserContext _userContext;

    public UserRepository(UserContext userContext)
    {
        _userContext = userContext;
    }

    public async Task AddAsync(User user)
    {
        await _userContext.Users.AddAsync(user);

        _userContext.SaveChanges();
    }

    public IQueryable<User> GetAllAsync()
    {
        return _userContext.Users.AsQueryable();
    }
}