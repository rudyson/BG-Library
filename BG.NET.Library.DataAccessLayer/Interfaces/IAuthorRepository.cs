using BG.NET.Library.Models.Entities.Library;

namespace BG.NET.Library.DataAccessLayer.Interfaces;

public interface IAuthorRepository : IGenericRepository<Author>
{
    Task<IEnumerable<Book>> GetBooks(int authorId);
    Task<bool> AddBook(int authorId, int bookId);
}