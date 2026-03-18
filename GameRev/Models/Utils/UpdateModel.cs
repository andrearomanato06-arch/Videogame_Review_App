using GameRev.DTOs.Requests.Update;
using GameRev.Models.Entities;

namespace GameRev.Models.Utils;

public static class UpdateModel
{
    public static void UpdateUserFromDto(User user, UpdateUserRequest request){
        if(request.Username is not null)
            user.Username = request.Username;
        if(request.Email is not null)
            user.Email = request.Email;
        if(request.Role is not null)
            user.Role = request.Role;
        if(request.Password is not null)
            user.Password = request.Password;
    }

    public static void UpdateAuthorFromDto (Author author, UpdateAuthorRequest request)
    {
        if(request.Name is not null)
            author.Name = request.Name;
    }

    public static void UpdatePlatformFromDto(Platform platform, UpdatePlatformRequest request)
    {
        if(platform is not null)
            platform.Name = request.Name;
    }

    public static void UpdateReviewFromDto (Review review, UpdateReviewRequest request)
    {
        if(request.Rating is not null)
            review.Rating = (double) request.Rating;
        if(request.Description is not null)
            review.Description = request.Description;        
    }

    public static void UpdateVideogameFromDto(Videogame videogame, UpdateVideogameRequest request, string? coverImage)
    {
        if(request.Title is not null)
            videogame.Title = request.Title;

        if(request.Description is not null)
            videogame.Description = request.Description;

        if(coverImage is not null)
            videogame.CoverImage = coverImage;

        if(request.ReleaseDate is not null)
            videogame.ReleaseDate = (DateOnly) request.ReleaseDate;                

        if(videogame.ReleaseDate <= DateOnly.FromDateTime(DateTime.Now))
            videogame.Released = true;
        else
            videogame.Released = false;
        
        if(request.Platforms is not null)
            videogame.Platforms = request.Platforms;

        if(request.AuthorId is not null)
            videogame.AuthorId = request.AuthorId;
    }
}