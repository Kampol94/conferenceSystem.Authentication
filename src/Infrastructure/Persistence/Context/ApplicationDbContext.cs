using System.Data;
using AuthenticationService.Application.Common.Interfaces;
using AuthenticationService.Domain.Common.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AuthenticationService.Infrastructure.Persistence.Context;

public abstract class ApplicationDbContext : DbContext
{
    protected readonly ICurrentUser _currentUser;
    private readonly DatabaseSettings _dbSettings;

    protected ApplicationDbContext(DbContextOptions options, ICurrentUser currentUser, IOptions<DatabaseSettings> dbSettings)
    {
        _currentUser = currentUser;
        _dbSettings = dbSettings.Value;
    }

    // Used by Dapper
    public IDbConnection Connection => Database.GetDbConnection();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}