namespace UserService.Domain.Users;

public interface IUserRepository
{
    Task AddAsync(User user);
    IQueryable<User> GetAllAsync();
}