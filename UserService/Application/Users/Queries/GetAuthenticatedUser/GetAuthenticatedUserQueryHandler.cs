﻿using Dapper;
using UserService.Application.Contracts;
using UserService.Application.Contracts.Queries;
using UserService.Application.Users.GetUser;

namespace UserService.Application.Users.GetAuthenticatedUser;

internal class GetAuthenticatedUserQueryHandler : IQueryHandler<GetAuthenticatedUserQuery, UserDto>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    private readonly IExecutionContextAccessor _executionContextAccessor;

    public GetAuthenticatedUserQueryHandler(
        ISqlConnectionFactory sqlConnectionFactory,
        IExecutionContextAccessor executionContextAccessor)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<UserDto> Handle(GetAuthenticatedUserQuery request, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sql = "SELECT" +
                           "[User].[Id], " +
                           "[User].[IsActive], " +
                           "[User].[Login], " +
                           "[User].[Email], " +
                           "[User].[Name] " +
                           "FROM [users].[v_Users] AS [User] " +
                           "WHERE [User].[Id] = @UserId";

        return await connection.QuerySingleAsync<UserDto>(sql, new
        {
            _executionContextAccessor.UserId
        });
    }
}