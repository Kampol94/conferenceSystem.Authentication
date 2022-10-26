using Microsoft.EntityFrameworkCore;
using UserService.Application.Contracts;

namespace UserService.Infrastructure.Domain;
public class Repository : IRepository
{
    private readonly UserContext _context;

    public Repository(UserContext context)
    {
        _context = context;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
