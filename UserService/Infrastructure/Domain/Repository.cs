using Microsoft.EntityFrameworkCore;
using UserService.Application.Contracts;
using UserService.Domain.Contracts;

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

    public List<BaseEntity> GetEntitiesWithEvents()
    {
        return _context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.Entity.CountEvents != 0)
                .Select(x => x.Entity)
                .ToList();
    }
}
