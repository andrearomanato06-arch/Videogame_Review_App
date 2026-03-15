namespace GameRev.Models.Entities;

using System;
using System.Collections.Generic;
using GameRev.Models.Auth;
using GameRev.Models.Utils;

public class User
{
    public long Id {get;set;}
    
    public string Username {get;set;}

    public string Email {get;set;}

    public string Password {get;set;}

    public DateTime RegistrationDate {get;set;} = DateTime.Now;

    public DateTime? LastAccess {get;set;} = null;

    public string Role {get;set;} = UserRole.BASIC;

    public List<Review> Reviews {get;set;} = [];

    public UserSession? UserSession {get;set;}
    public long? Jtid {get;set;}
}

