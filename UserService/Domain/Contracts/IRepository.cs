namespace UserService.Application.Contracts;
public interface IRepository
{
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}
