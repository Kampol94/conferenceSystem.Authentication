using UserService.Application.Authorization.GetUserPermissions;
using UserService.Application.Contracts.Queries;

namespace UserService.Application.Authorization.GetAuthenticatedUserPermissions;

public class GetAuthenticatedUserPermissionsQuery : QueryBase<List<UserPermissionDto>>
{
}