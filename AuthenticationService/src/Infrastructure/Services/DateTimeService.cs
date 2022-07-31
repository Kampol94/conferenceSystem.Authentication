using AuthenticationService.Application.Common.Interfaces;

namespace AuthenticationService.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
