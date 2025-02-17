﻿using Dapper;
using UserService.Application.Contracts;
using UserService.Domain.UserRegistrations;

namespace UserService.Application.UserRegistrations;

public class UsersCounter : IUsersCounter
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public UsersCounter(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public int CountUsersWithLogin(string login)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sql = "SELECT " +
                           "COUNT(*) " +
                           "FROM [users].[Users] AS [User]" +
                           "WHERE [User].[Login] = @Login";
        return connection.QuerySingle<int>(
            sql,
            new
            {
                login
            });
    }
}