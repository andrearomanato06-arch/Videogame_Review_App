using GameRev.DTOs.Mappers;
using GameRev.DTOs.Requests;
using GameRev.DTOs.Requests.Update;
using GameRev.DTOs.Responses;
using GameRev.Models.Utils;
using GameRev.Repository.Entities.Interfaces;
using GameRev.Services.Interfaces;

namespace GameRev.Services.Entities;

public class PlatformService : IPlatformService
{

    private readonly IPlatformRepository platformRepository;

    public PlatformService(IPlatformRepository platformRepository)
    {
        this.platformRepository = platformRepository;
    }

    public async Task<PlatformResponse?> AddAsync(PlatformRequest request, CancellationToken ct)
    {
        var platform = DtosToModels.PlatformRequestToPlatform(request);
        var response = await platformRepository.AddAsync(platform,ct);
        return response is not null 
        ? ModelsToDtos.PlatformToPlatformResponse(response)
        : null;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken ct)
    {
        var platform = await platformRepository.GetByIdAsync(id,ct);
        if(platform is null) return false;
        return await platformRepository.DeleteAsync(platform,ct);
    }

    public async Task<List<PlatformResponse>> GetAllAsync(CancellationToken ct)
    {
        var platforms = await platformRepository.GetAllAsync(ct);
        return ModelsToDtos.PlatformToPlatformResponse(platforms);   
    }

    public async Task<PlatformResponse?> GetByIdAsync(long id, CancellationToken ct)
    {
        var platform = await platformRepository.GetByIdAsync(id,ct);
        return platform is not null
        ? ModelsToDtos.PlatformToPlatformResponse(platform)
        : null;
    }

    public async Task<PlatformResponse?> GetByNameAsync(string name, CancellationToken ct)
    {
        var platform = await platformRepository.GetByNameAsync(name,ct);
        return platform is not null
        ? ModelsToDtos.PlatformToPlatformResponse(platform)
        : null;
    }

    public async Task<bool> UpdatePlatformAsync(UpdatePlatformRequest request, CancellationToken ct)
    {
        var platform = await platformRepository.GetByIdAsync(request.Id,ct);
        if(platform is null) return false;
        UpdateModel.UpdatePlatformFromDto(platform,request);
        return await platformRepository.UpdateAsync(platform,ct);
    }
}