using Microsoft.EntityFrameworkCore;
using UserService.Domain.UserRegistrations;

namespace UserService.Infrastructure.Domain.UserRegistrations;

public class UserRegistrationRepository : IUserRegistrationRepository
{
    private readonly UserContext _userContext;

    public UserRegistrationRepository(UserContext userContext)
    {
        _userContext = userContext;
    }

    public async Task AddAsync(UserRegistration userRegistration)
    {
        await _userContext.AddAsync(userRegistration);
    }

    public async Task<UserRegistration> GetByIdAsync(UserRegistrationId userRegistrationId)
    {
        return await _userContext.UserRegistrations.FirstOrDefaultAsync(x => x.Id == userRegistrationId);
    }
}