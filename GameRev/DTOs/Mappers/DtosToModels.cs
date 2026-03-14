using GameRev.DTOs.Requests;
using GameRev.Models.Entities;

namespace GameRev.DTOs.Mappers;

public static class DtosToModels
{
    public static Platform PlatformRequestToPlatform (PlatformRequest request)
    {
        return new Platform
        {
            Name = request.Name
        };
    }

    public static Author AuthorRequestToAuthor (AuthorRequest request)
    {
        return new Author
        {
            Name = request.Name
        };
    }

    public static User UserRequestToUser (UserRequest request)
    {
        return new User
        {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password,
            RegistrationDate = request.RegistrationDate,
            Role = request.Role
        };
    }

    public static Review ReviewRequestToReview (ReviewRequest request)
    {
        return new Review
        {
            Rating = request.Rating is not null ? (double) request.Rating : 0.0,
            Description = request.Description is not null ? request.Description :  "",
            ReviewDate = request.ReviewDate,
            VideogameId = request.VideogameId,
            UserId = request.UserId
        };
    }

    public static Videogame VideogameRequestToVideogame (VideogameRequest request)
    {
        return new Videogame
        {
            Title = request.Title,
            Description = request.Description,
            CoverImage = request.CoverImage,
            Objectives = request.Objectives,
            ReleaseDate = request.ReleaseDate,
            Released = request.Released,
            Platforms = request.Platforms,
            AuthorId = request.AuthorId
        };
    }
} 