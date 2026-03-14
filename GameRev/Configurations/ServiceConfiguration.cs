using GameRev.Services.Entities;
using GameRev.Services.Entities.Interfaces;
using GameRev.Services.Interfaces;

namespace GameRev.Configurations;

public static class ServiceConfiguration
{
    public static IServiceCollection AddServicesCollection(this IServiceCollection service)
    {
        service.AddScoped<IAuthorService, AuthorService>();
        service.AddScoped<IPlatformService,PlatformService>();
        service.AddScoped<IUserService,UserService>();
        service.AddScoped<IVideogameService, VideogameService>();
        service.AddScoped<IReviewService, ReviewService>();
        return service;
    }
}