using Microsoft.EntityFrameworkCore;
using GameRev.Data;

namespace GameRev.Configurations;

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