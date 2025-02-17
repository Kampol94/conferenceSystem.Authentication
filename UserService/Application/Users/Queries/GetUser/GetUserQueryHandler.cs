﻿using Dapper;
using UserService.Application.Contracts;
using UserService.Application.Contracts.Queries;

namespace UserService.Application.Users.GetUser;

internal class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetUserQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sql = "SELECT" +
                           "[User].[Id], " +
                           "[User].[IsActive], " +
                           "[User].[Login], " +
                           "[User].[Email], " +
                           "[User].[Name] " +
                           "FROM [users].[Users] AS [User] " +
                           "WHERE [User].[Id] = @UserId";

        return await connection.QuerySingleAsync<UserDto>(sql, new
        {
            request.UserId
        });
    }
}