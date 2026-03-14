using GameRev.DTOs.Responses;
using GameRev.Models.Entities;

namespace GameRev.DTOs.Mappers;

public static class ModelsToDtos
{
    
    public static AuthorResponse AuthorToAuthorResponse (Author author)
    {
        return new AuthorResponse
        {
            Id = author.Id,
            Name = author.Name
        };
    }

    public static List<AuthorResponse> AuthorToAuthorResponse (List<Author> authors)
    {
        List<AuthorResponse> response =  [];
        foreach(Author author in authors)
        {
            response.Add(new AuthorResponse
            {
                Id = author.Id,
                Name = author.Name
            });
        }
        return response;
    }

    public static PlatformResponse PlatformToPlatformResponse (Platform platform)
    {
        return new PlatformResponse
        {
            Id = platform.Id,
            Name = platform.Name
        };
    }

    public static List<PlatformResponse> PlatformToPlatformResponse (List<Platform> platforms)
    {
        List<PlatformResponse> response = [];
        foreach(Platform platform in platforms)
        {
            response.Add(new PlatformResponse
            {
                Id = platform.Id,
                Name = platform.Name
            });
        }
        return response;
    }

    public static UserResponse UserToUserResponse (User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };
    }

    public static List<UserResponse> UserToUserResponse (List<User> users)
    {
        List<UserResponse> response = [];
        foreach(User user in users)
        {
            response.Add(new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            });
        }
        return response;
    }

    public static ReviewResponse ReviewToReviewResponse (Review review)
    {
        return new ReviewResponse
        {
            Id = review.Id,
            Rating = review.Rating,
            Description = review.Description,
            ReviewDate = review.ReviewDate,
            VideogameId = review.VideogameId,
            UserId = review.UserId
        };
    }

    public static List<ReviewResponse> ReviewToReviewResponse (List<Review> reviews)
    {
        List<ReviewResponse> response = [];
        foreach(Review review in reviews)
        {
            response.Add( new ReviewResponse
            {
                Id = review.Id,
                Rating = review.Rating,
                Description = review.Description,
                ReviewDate = review.ReviewDate,
                VideogameId = review.VideogameId,
                UserId = review.UserId
            });
        }
        return response;
    }

    public static VideogameResponse VideogameToVideogameResponse (Videogame videogame)
    {
        return new VideogameResponse
        {
            Id = videogame.Id,
            Title = videogame.Title,
            Description = videogame.Description,
            CoverImage = videogame.CoverImage,
            Objectives = videogame.Objectives,
            ReleaseDate = videogame.ReleaseDate,
            Released = videogame.Released,
            Platforms = videogame.Platforms,
            AuthorId = videogame.AuthorId
        };
    }

    public static List<VideogameResponse> VideogameToVideogameResponse (List<Videogame> videogames)
    {
        List<VideogameResponse> response = [];
        foreach(Videogame videogame in videogames)
        {
            response.Add(new VideogameResponse
            {
                Id = videogame.Id,
                Title = videogame.Title,
                Description = videogame.Description,
                CoverImage = videogame.CoverImage,
                Objectives = videogame.Objectives,
                ReleaseDate = videogame.ReleaseDate,
                Released = videogame.Released,
                Platforms = videogame.Platforms,
                AuthorId = videogame.AuthorId            
            });
        }
        return response;
    }
}