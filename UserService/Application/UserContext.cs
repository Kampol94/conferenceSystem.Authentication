using UserService.Application.Contracts;
using UserService.Domain.Users;

namespace UserService.Application;
public class UserContext : IUserContext
{
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public UserContext(IExecutionContextAccessor executionContextAccessor)
    {
        _executionContextAccessor = executionContextAccessor;
    }

    public UserId UserId => new UserId(_executionContextAccessor.UserId);
}
