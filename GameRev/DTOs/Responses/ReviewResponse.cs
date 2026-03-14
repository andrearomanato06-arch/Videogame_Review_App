namespace GameRev.DTOs.Responses;

public class ReviewResponse
{
    public long Id {get;set;}

    public double Rating {get;set;}

    public string Description {get;set;}
    
    public DateTime ReviewDate {get;set;}

    public long VideogameId {get;set;}

    public long? UserId{get;set;}
   
}