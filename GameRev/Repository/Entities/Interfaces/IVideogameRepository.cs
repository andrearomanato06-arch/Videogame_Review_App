using GameRev.DTOs.Filters;
using GameRev.DTOs.Responses;
using GameRev.Models.Entities;
using GameRev.Repository.Generic;

namespace GameRev.Repository.Entities.Interfaces;

public interface IVideogameRepository : IGenericCrudRepository<Videogame>
{
    Task<Videogame?> GetByTitleAsync (string title, CancellationToken ct);

    Task<List<Videogame>> GetByPlatformAsync (string platform, CancellationToken ct);

    Task<PagedResponse<MinimalVideogameResponse>> GetNewAsync (int page, int elementsToShow, CancellationToken ct);

    Task<PagedResponse<MinimalVideogameResponse>> GetMostLikedAsync (int page, int elementsToShow, CancellationToken ct);

    Task<PagedResponse<MinimalVideogameResponse>> SearchAsync (VideogameSearchFilter filter,int page, int elementsToShow, CancellationToken ct);

    Task<List<MinimalVideogameResponse>> GetCasualGames (int limit, CancellationToken ct);
}