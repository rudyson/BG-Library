using BG.NET.Library.BusinessLogicLayer.Interfaces;
using BG.NET.Library.Models.Dto.Library;

namespace BG.NET.Library.BusinessLogicLayer.Services;

public class BookService : IBookService
{

    public async Task<int> Create(BookDtoNew book)
    {
        throw new NotImplementedException();
    }

    public async Task<BookDtoShort> FindShort(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<BookDtoFull> FindFull(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<BookDtoShort>> AllShort()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<BookDtoFull>> AllFull()
    {
        throw new NotImplementedException();
    }

    public async Task<BookDtoShort> Update(int id, BookDtoNew book)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}