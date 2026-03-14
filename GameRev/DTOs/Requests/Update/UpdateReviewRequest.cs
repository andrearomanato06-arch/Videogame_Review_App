namespace GameRev.DTOs.Requests.Update;

public class UpdateReviewRequest
{
    public long Id {get;set;}

    public double? Rating {get;set;} = null;

    public string? Description {get;set;} = null;
}