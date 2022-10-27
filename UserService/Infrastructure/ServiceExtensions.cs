using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Contracts;
using UserService.Domain.UserRegistrations;
using UserService.Domain.Users;
using UserService.Infrastructure.Domain;
using UserService.Infrastructure.Domain.UserRegistrations;
using UserService.Infrastructure.Domain.Users;
using UserService.Infrastructure.Services;

namespace UserService.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration != null)
        {
            services.AddDbContext<UserContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        services.AddTransient<IUserRegistrationRepository, UserRegistrationRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IRepository, Repository>();
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddTransient<IEventBus, EventBus>();
        services.AddTransient<ISqlConnectionFactory, SqlConnectionFactory>(x => new SqlConnectionFactory(configuration.GetConnectionString("DefaultConnection")));
        services.AddHostedService<EventReceiverService>();

        return services;
    }
}
