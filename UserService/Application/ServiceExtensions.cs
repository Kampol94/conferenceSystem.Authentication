using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UserService.Application.Contracts.Commands;
using UserService.Application.UserRegistrations;
using UserService.Domain.UserRegistrations;
using UserService.Domain.Users;

namespace UserService.Application;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CommitBehavior<,>));
        services.AddTransient<IUsersCounter, UsersCounter>();
        return services;
    }
}
