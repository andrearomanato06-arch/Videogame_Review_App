using GameRev.Models.Entities;

namespace GameRev.DTOs.Responses;

public class VideogameResponse
{
    public long Id {get;set;}

    public string Title {get;set;}

    public string Description {get;set;}

    public string CoverImage {get;set;}

    public int Objectives {get;set;}

    public DateOnly ReleaseDate {get;set;}

    public bool Released {get;set;} = true;

    public List <Platform> Platforms {get;set;} = [];

    public long? AuthorId {get;set;} 
}