namespace GameRev.DTOs.Responses;

using System.Collections.Generic;
public class PagedResponse<T>
{
    public IEnumerable<T> Elements {get; set;}
    
    public int CurrentPage {get;set;}

    public int TotalElements {get;set;}

    public int TotalPages {get;set;}
}