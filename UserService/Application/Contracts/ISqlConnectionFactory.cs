using System.Data;

namespace UserService.Application.Contracts;

public interface ISqlConnectionFactory
{
    IDbConnection GetOpenConnection();
}