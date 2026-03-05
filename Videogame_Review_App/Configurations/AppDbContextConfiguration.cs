using Microsoft.EntityFrameworkCore;
using Videogame_Review_App.Data;

namespace Videogame_Review_App.Configurations;

public static class AppDbContextConfiguration
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => { 
                var connectionString = configuration.GetConnectionString("Server");
                var serverVersion = ServerVersion.AutoDetect(connectionString);
                options.UseMySql( connectionString, serverVersion);
            }
        );
        return services;
    }
}