using BG.NET.Library.Models.Entities.Library;

namespace BG.NET.Library.DataAccessLayer.Interfaces;

public interface IBookRepository : IGenericRepository<Book>
{
    Task<Author?> GetAuthor(int bookId);
    Task<bool> SetAuthor(int bookId,int authorId);
}