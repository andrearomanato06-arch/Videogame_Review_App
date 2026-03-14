using GameRev.DTOs.Mappers;
using GameRev.DTOs.Requests;
using GameRev.DTOs.Requests.Update;
using GameRev.DTOs.Responses;
using GameRev.Models.Utils;
using GameRev.Repository.Entities.Interfaces;
using GameRev.Services.Entities.Interfaces;

namespace GameRev.Services.Entities;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository authorRepository;

    public AuthorService (IAuthorRepository authorRepository)
    {
        this.authorRepository = authorRepository;
    }

    public async Task<AuthorResponse?> AddAsync(AuthorRequest request, CancellationToken ct)
    {
        var author = DtosToModels.AuthorRequestToAuthor(request);
        var response = await authorRepository.AddAsync(author,ct);
        return response is not null
        ? ModelsToDtos.AuthorToAuthorResponse(author)
        : null;
    }

    public async Task<List<AuthorResponse>> GetAllAsync(CancellationToken ct)
    {
        var authors = await authorRepository.GetAllAsync(ct);
        return ModelsToDtos.AuthorToAuthorResponse(authors);
    }

    public async Task<AuthorResponse?> GetByIdAsync(long id, CancellationToken ct)
    {
        var author = await authorRepository.GetByIdAsync(id,ct);
        return author is not null ?
        ModelsToDtos.AuthorToAuthorResponse(author)
        : null;
    }

    public async Task<bool> RemoveAsync(long id, CancellationToken ct)
    {
        var author = await authorRepository.GetByIdAsync(id,ct);
        if(author is null) return false;
        return await authorRepository.DeleteAsync(author,ct);
    }

    public async Task<bool> UpdateAsync(UpdateAuthorRequest request, CancellationToken ct)
    {
        var author = await authorRepository.GetByIdAsync(request.Id,ct);
        if(author is null) return false;
        UpdateModel.UpdateAuthorFromDto(author,request);
        return await authorRepository.UpdateAsync(author,ct);
    }
}