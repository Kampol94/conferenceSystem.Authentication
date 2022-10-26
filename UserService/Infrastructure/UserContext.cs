using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserService.Domain.UserRegistrations;
using UserService.Domain.Users;
using UserService.Infrastructure.Domain.UserRegistrations;
using UserService.Infrastructure.Domain.Users;

namespace UserService.Infrastructure;

public class UserContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;

    public DbSet<UserRegistration> UserRegistrations { get; set; }

    public DbSet<User> Users { get; set; }

    public UserContext(DbContextOptions options, ILoggerFactory loggerFactory)
        : base(options)
    {
        _loggerFactory = loggerFactory;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserRegistrationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }
}