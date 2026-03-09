namespace GameRev.Repository.Entities;

using GameRev.Repository.Generic;
using GameRev.Models;
using GameRev.Data;
using GameRev.Repository.Entities.Interfaces;

public class AuthorRepository : GenericCrudRepository<Author>, IAuthorRepository
{
    public AuthorRepository(AppDbContext context) : base(context) {}
}