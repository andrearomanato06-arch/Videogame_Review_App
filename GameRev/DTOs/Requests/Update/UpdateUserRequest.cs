using GameRev.Models.Utils;

namespace GameRev.DTOs.Requests.Update;

public class UpdateUserRequest{

    public long Id {get;set;}
    
    public string? Username {get;set;}

    public string? Email {get;set;}

    public string? Password {get;set;}

    public string? Role {get;set;} = UserRole.BASIC;   
}