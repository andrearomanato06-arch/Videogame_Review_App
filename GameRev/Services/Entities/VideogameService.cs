using GameRev.DTOs.Filters;
using GameRev.DTOs.Mappers;
using GameRev.DTOs.Requests;
using GameRev.DTOs.Requests.Update;
using GameRev.DTOs.Responses;
using GameRev.Models.Utils;
using GameRev.Repository.Entities;
using GameRev.Repository.Entities.Interfaces;
using GameRev.Services.Entities.Interfaces;

namespace GameRev.Services.Entities;

public class VideogameService : IVideogameService
{
    private readonly IVideogameRepository videogameRepository;

    private readonly IPlatformRepository platformRepository;
    private readonly ILogger<VideogameService> logger;
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly List<string> validExtensions = new List<string> {"jpeg","png","jpg","webp"};

    public VideogameService(IVideogameRepository videogameRepository, IWebHostEnvironment webHostEnvironment, ILogger<VideogameService> logger, IPlatformRepository platformRepository)
    {
        this.videogameRepository = videogameRepository;
        this.webHostEnvironment = webHostEnvironment;
        this.logger = logger;
        this.platformRepository = platformRepository;
    }

    public async Task<VideogameResponse?> AddAsync(VideogameRequest request, CancellationToken ct)
    {
        string? path = await SaveCoverImage(request.CoverImage, request.Title);
        if(path is null)
        {
            logger.LogError("Failed to get the image path because it's  null");
            return null;
        }

        var videogame = await DtosToModels.VideogameRequestToVideogame(request, path, platformRepository);
        var response = await videogameRepository.AddVideogameAsync(videogame,ct);
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
        if(videogame is null)
        {
            logger.LogError("Failed to fetch videogame with title: ${Title}", title);
            return null;
        }
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
        if(videogame is null)
        {
            logger.LogError("Failed to find videogame with ID: ${Id}", id);
            return false;
        }
        return await videogameRepository.DeleteAsync(videogame,ct);
    }

    public async Task<PagedResponse<MinimalVideogameResponse>> SearchAsync(VideogameSearchFilter filter,int page, int elementsToShow, CancellationToken ct)
    {
        return await videogameRepository.SearchAsync(filter,page,elementsToShow,ct);
    }

    public async Task<bool> UpdateAsync(UpdateVideogameRequest request, CancellationToken ct)
    {
        var videogame = await videogameRepository.GetByIdAsync(request.Id, ct);
        if(videogame is null)
        {
            logger.LogError("Failed to find videogame with the specified ID: {Id}", request.Id);
            return false;
        }
        
        string? newImgPath = null;
        if(request.CoverImage is not null && request.Title is not null)
        {
            newImgPath = await SaveCoverImage(request.CoverImage, request.Title);
            if(newImgPath is null)
            {
                logger.LogError("Failed to get the image path because it's  null");
                return false;
            }
        } 

        UpdateModel.UpdateVideogameFromDto(videogame, request, newImgPath, platformRepository);
        return await videogameRepository.UpdateAsync(videogame,ct);
    }

    public async Task<List<MinimalVideogameResponse>> GetCasualGames (int limit, CancellationToken ct)
    {
        return await videogameRepository.GetCasualGames(limit,ct);
    }

    private async Task<string?> SaveCoverImage (IFormFile image, string videogameTitle)
    {
        string? physicalPath;
        string? relativeUrl;
        logger.LogInformation("File Name: {Ext}", image.FileName);
        var pathToStr = image.FileName.ToString();
        var fileExtIndex = pathToStr.LastIndexOf("."); // get the extension by searching what's next the last "."
        var extension = pathToStr.Split(".", fileExtIndex);
        try
        {
            if (!validExtensions.Contains(extension[1]))
            {
                logger.LogError("Invalid Exception provided");
                return null;
            }

            var fileName = videogameTitle.ToLower().Trim().Replace(" ", "-") + ".webp";
            fileName = fileName.Replace(":", "");
            physicalPath = Path.Combine(webHostEnvironment.WebRootPath, "videogames", "covers", fileName);
            relativeUrl = Path.Combine("videogames", "covers", fileName).Replace("\\", "/");

            if (File.Exists(physicalPath))
            {
                File.Delete(physicalPath);
            }

            using (var stream = new FileStream(physicalPath, FileMode.Create))
            {
                await image.CopyToAsync(stream);    
            }
                
        }catch(Exception e)
        {
            logger.LogError(e, "An error occurred while saving the image");
            return null;
        }    

        return relativeUrl;
    }
}