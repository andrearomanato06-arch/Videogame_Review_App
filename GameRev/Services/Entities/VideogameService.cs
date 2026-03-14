using GameRev.DTOs.Filters;
using GameRev.DTOs.Mappers;
using GameRev.DTOs.Requests;
using GameRev.DTOs.Requests.Update;
using GameRev.DTOs.Responses;
using GameRev.Models.Utils;
using GameRev.Repository.Entities.Interfaces;
using GameRev.Services.Entities.Interfaces;

namespace GameRev.Services.Entities;

public class VideogameService : IVideogameService
{
    private readonly IVideogameRepository videogameRepository;

    public VideogameService(IVideogameRepository videogameRepository)
    {
        this.videogameRepository = videogameRepository;
    }
    public  async Task<VideogameResponse?> AddAsync(VideogameRequest request, CancellationToken ct)
    {
        var videogame = DtosToModels.VideogameRequestToVideogame(request);
        var response = await videogameRepository.AddAsync(videogame,ct);
        return response is not null
        ? ModelsToDtos.VideogameToVideogameResponse(response)
        : null;
    }

    public async Task<List<VideogameResponse>> GetAllAsync(CancellationToken ct)
    {
        var videogames = await videogameRepository.GetAllAsync(ct);
        return ModelsToDtos.VideogameToVideogameResponse(videogames);
    }

    public async Task<VideogameResponse?> GetByIdAsync(long id, CancellationToken ct)
    {
        var videogame = await videogameRepository.GetByIdAsync(id,ct);
        return videogame is not null
        ? ModelsToDtos.VideogameToVideogameResponse(videogame)
        : null;
    }

    public async Task<List<VideogameResponse>> GetByPlatformAsync(string platform, CancellationToken ct)
    {
        var videogames = await videogameRepository.GetByPlatformAsync(platform,ct);
        return ModelsToDtos.VideogameToVideogameResponse(videogames);
    }

    public async Task<VideogameResponse?> GetByTitleAsync(string title, CancellationToken ct)
    {
        var videogame = await videogameRepository.GetByTitleAsync(title,ct);
        if(videogame is null) return null;
        return ModelsToDtos.VideogameToVideogameResponse(videogame);
    }

    public async Task<List<VideogameResponse>> GetMostLikedAsync(int elementsToShow, CancellationToken ct)
    {
        var videogames = await videogameRepository.GetMostLikedAsync(elementsToShow, ct);
        return ModelsToDtos.VideogameToVideogameResponse(videogames);
    }

    public async Task<List<VideogameResponse>> GetNewAsync(CancellationToken ct)
    {
        var videogames = await videogameRepository.GetNewAsync(ct);
        return ModelsToDtos.VideogameToVideogameResponse(videogames);
    }

    public async Task<bool> RemoveAsync(long id, CancellationToken ct)
    {
        var videogame = await videogameRepository.GetByIdAsync(id,ct);
        if(videogame is null) return false;
        return await videogameRepository.DeleteAsync(videogame,ct);
    }

    public async Task<List<VideogameResponse>> SearchAsync(VideogameSearchFilter filter, CancellationToken ct)
    {
        var videogames = await videogameRepository.SearchAsync(filter,ct);
        return ModelsToDtos.VideogameToVideogameResponse(videogames);
    }

    public async Task<bool> UpdateAsync(UpdateVideogameRequest request, CancellationToken ct)
    {
        var videogame = await videogameRepository.GetByIdAsync(request.Id, ct);
        if(videogame is null) return false;
        UpdateModel.UpdateVideogameFromDto(videogame,request);
        return await videogameRepository.UpdateAsync(videogame,ct);
    }
}