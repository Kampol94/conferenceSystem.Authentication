using UserService.Domain.Contracts;

namespace UserService.Application.Contracts;
public interface IRepository
{
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
    List<BaseEntity> GetEntitiesWithEvents();
}
