namespace GameRev.Models.Auth;
using GameRev.Models.Entities;
public class UserSession
{
    public long Jtid {get;set;}

    public DateTime IssuedAt {get;set;}

    public User? User {get;set;}
    public long? UserId {get;set;}
}