using GameRev.Models.Utils;

namespace GameRev.DTOs.Requests;

public class UserRequest
{
    public string Username {get;set;}

    public string Email {get;set;}

    public string Password {get;set;}

    public DateTime RegistrationDate {get;set;} = DateTime.Now;
 
    public string Role {get;set;} = UserRole.BASIC;
}