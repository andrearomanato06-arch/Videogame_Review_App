namespace GameRev.Models.Entities;

using System.Collections.Generic;

public class Author
{
    public long Id {get;set;}

    public string Name {get;set;}
    
    public List<Videogame> Videogames {get;set;} = [] ;
}