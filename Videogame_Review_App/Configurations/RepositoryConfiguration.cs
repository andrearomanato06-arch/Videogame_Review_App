using Videogame_Review_App.Repository.Entities;
using Videogame_Review_App.Repository.Entities.Interfaces;

namespace Videogame_Review_App.Configurations;

public static class RepositoryConfiguration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}