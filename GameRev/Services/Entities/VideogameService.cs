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
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly List<string> validExtensions = new List<string> {".jpeg",".png",".jpg",".webp"};

    public VideogameService(IVideogameRepository videogameRepository, IWebHostEnvironment webHostEnvironment)
    {
        this.videogameRepository = videogameRepository;
        this.webHostEnvironment = webHostEnvironment;
    }

    public async Task<VideogameResponse?> AddAsync(VideogameRequest request, CancellationToken ct)
    {
        string? path = await SaveCoverImage(request.CoverImage, request.Title);
        if(path is null)
        {
            //log
            return null;
        }

        var videogame = DtosToModels.VideogameRequestToVideogame(request, path);
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

    public async Task<PagedResponse<MinimalVideogameResponse>> GetMostLikedAsync(int page, int elementsToShow, CancellationToken ct)
    {
        return await videogameRepository.GetMostLikedAsync(page, elementsToShow, ct);
    }

    public async Task<PagedResponse<MinimalVideogameResponse>> GetNewAsync(int page, int elementsToShow, CancellationToken ct)
    {
        return await videogameRepository.GetNewAsync(page, elementsToShow, ct);
    }

    public async Task<bool> RemoveAsync(long id, CancellationToken ct)
    {
        var videogame = await videogameRepository.GetByIdAsync(id,ct);
        if(videogame is null) return false;
        return await videogameRepository.DeleteAsync(videogame,ct);
    }

    public async Task<PagedResponse<MinimalVideogameResponse>> SearchAsync(VideogameSearchFilter filter,int page, int elementsToShow, CancellationToken ct)
    {
        return await videogameRepository.SearchAsync(filter,page,elementsToShow,ct);
    }

    public async Task<bool> UpdateAsync(UpdateVideogameRequest request, CancellationToken ct)
    {
        var videogame = await videogameRepository.GetByIdAsync(request.Id, ct);
        if(videogame is null) return false;

        string? newImgPath = null;
        if(request.CoverImage is not null && request.Title is not null)
        {
            newImgPath = await SaveCoverImage(request.CoverImage, request.Title);
            if(newImgPath is null) return false;
        }

        UpdateModel.UpdateVideogameFromDto(videogame, request, newImgPath);
        return await videogameRepository.UpdateAsync(videogame,ct);
    }

    public async Task<List<MinimalVideogameResponse>> GetCasualGames (int limit, CancellationToken ct)
    {
        return await videogameRepository.GetCasualGames(limit,ct);
    }

    private async Task<string?> SaveCoverImage (IFormFile image, string videogameTitle)
    {
        string? path;
        try
        {
            if (!validExtensions.Contains(image.FileName))
            {
                return null;
                //log invalid extension provided
            }

            var fileName = videogameTitle.ToLower().Trim().Replace(" ", "-") + ".webp";
            path = Path.Combine(webHostEnvironment.WebRootPath, "videogames", "covers", fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);    
            }
                
        }catch(Exception e)
        {
            //log error
            Console.WriteLine(e);
            return null;
        }    

        return path;
    }
}