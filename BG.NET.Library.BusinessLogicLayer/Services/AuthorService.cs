using AutoMapper;
using BG.NET.Library.BusinessLogicLayer.Interfaces;
using BG.NET.Library.DataAccessLayer.Interfaces;
using BG.NET.Library.Models.Dto.Library;
using BG.NET.Library.Models.Entities.Library;

namespace BG.NET.Library.BusinessLogicLayer.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;

    public AuthorService(IAuthorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<AuthorDtoNoBooks>> AllShort()
    {
        var authors = await _repository.GetAll();
        var mappedDto = _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorDtoNoBooks>>(authors);
        return mappedDto;
    }

    public async Task<IEnumerable<AuthorDtoFull>> AllFull()
    {
        throw new NotImplementedException();
    }

    public async Task<AuthorDtoNoBooks> FindShort()
    {
        throw new NotImplementedException();
    }

    public async Task<AuthorDtoFull> FindFull()
    {
        throw new NotImplementedException();
    }

    public async Task<AuthorDtoNoBooks> Create(AuthorDtoBase author)
    {
        throw new NotImplementedException();
    }

    public async Task<AuthorDtoNoBooks> Update(int id, AuthorDtoBase author)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<AuthorDtoFull> Books(int id)
    {
        throw new NotImplementedException();
    }
}