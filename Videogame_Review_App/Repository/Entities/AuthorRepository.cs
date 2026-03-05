namespace Videogame_Review_App.Repository.Entities;

using Videogame_Review_App.Repository.Generic;
using Videogame_Review_App.Models;
using Videogame_Review_App.Data;
using Videogame_Review_App.Repository.Entities.Interfaces;

public class AuthorRepository : GenericCrudRepository<Author>, IAuthorRepository
{
    public AuthorRepository(AppDbContext context) : base(context) {}
}