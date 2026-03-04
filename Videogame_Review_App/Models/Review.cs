namespace VideogameReviewApp.Models;

using System; 
using System.Collections.Generic;
public class Review
{
    public long Id {get;set;}

    public int Score {get;set;}

    public string Description {get;set;}
    
    public DateTime ReviewDate {get;set;}

    public long VideogameId {get;set;}
    public Videogame Videogame {get;set;}

    public long? UserId{get;set;}
    public User User {get;set;}

}