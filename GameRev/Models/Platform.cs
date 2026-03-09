namespace GameRev.Models;

public class Platform
{
    public long Id {get;set;}

    public string Name {get;set;}

    public List<Videogame> Videogames {get;set;} = [];

    public List<VideogamePlatform> VideogamePlatforms {get;set;} = [];

}