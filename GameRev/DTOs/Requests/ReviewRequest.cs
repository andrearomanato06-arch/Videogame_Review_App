namespace GameRev.DTOs.Requests;

public class ReviewRequest
{
    public double? Rating {get;set;} = null;

    public string? Description {get;set;} = null;

    public DateTime ReviewDate {get;set;} = DateTime.Now;

    public long VideogameId {get;set;}

    public long UserId {get;set;}
}

