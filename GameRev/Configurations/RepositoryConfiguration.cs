using GameRev.Repository.Entities;
using GameRev.Repository.Entities.Interfaces;

namespace GameRev.Configurations;

public static class RepositoryConfiguration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}