using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure;

namespace UserService.API;

public static class MigrationManager
{
    public static WebApplication MigrateDatabase(this WebApplication webApp)
    {
        using (var scope = webApp.Services.CreateScope())
        {
            using (var appContext = scope.ServiceProvider.GetRequiredService<UserContext>())
            {
                try
                {
                    appContext.Database.Migrate();
                }
                catch (Exception)
                {
                    //TODO: handle
                    throw;
                }
            }
        }
        return webApp;
    }
}
