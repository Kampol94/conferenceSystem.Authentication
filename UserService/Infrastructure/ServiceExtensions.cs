using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Contracts;
using UserService.Domain.ExhibitionProposals;
using UserService.Domain.Members;
using UserService.Infrastructure.Domain.MeetingGroupProposals;
using UserService.Infrastructure.Domain.Members;
using UserService.Infrastructure.Services;

namespace UserService.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration != null)
        {
            services.AddDbContext<ManagementContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        services.AddTransient<IExhibitionProposalsRepository, ExhibitionProposalRepository>();
        services.AddTransient<IMemberRepository, MemberRepository>();
        services.AddTransient<IEventService, EventService>();
        services.AddTransient<ISqlConnectionFactory, SqlConnectionFactory>(x => new SqlConnectionFactory(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
