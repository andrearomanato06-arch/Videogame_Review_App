namespace GameRev.Models;

using System;
using System.Collections.Generic;

public class Author
{
    public long Id {get;set;}

    public string Name {get;set;}
    
    public List<Videogame> Videogames {get;set;} = [] ;
}