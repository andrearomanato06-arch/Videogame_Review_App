namespace VideogameReviewApp.Models;

using System; 
using System.Collections.Generic;
public class User
{
    public long Id {get;set;}
    
    public string Username {get;set;}

    public string Email {get;set;}

    public string Password {get;set;}

    public DateTime RegistrationDate {get;set;}

    public DateTime LastAccess {get;set;}

    public List<Review> Reviews {get;set;} = [];
}