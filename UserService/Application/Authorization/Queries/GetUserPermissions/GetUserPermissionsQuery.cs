using UserService.Application.Contracts.Queries;

namespace UserService.Application.Authorization.GetUserPermissions;

public class GetUserPermissionsQuery : QueryBase<List<UserPermissionDto>>
{
    public GetUserPermissionsQuery(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}