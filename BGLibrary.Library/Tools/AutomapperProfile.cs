using AutoMapper;
using BG.NET.Library.Models.Dto.Library;
using BG.NET.Library.Models.Entities.Library;

namespace BGLibrary.Library.Tools;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        // Deprecated
        CreateMap<NewBookDto, Book>();
        CreateMap<UpdateBookDto, Book>();
        CreateMap<NewAuthorDto, Author>();
        // DTO -> Author
        CreateMap<AuthorDtoBase, Author>();
        CreateMap<AuthorDtoFull, Author>();
        CreateMap<AuthorDtoNoBooks, Author>();
        // Author -> DTO
        CreateMap<Author, AuthorDtoNoBooks>()
            .ForMember(d => d.Books, 
                o => o
                    .MapFrom(x => x.Books.Count()));
        CreateMap<Author, AuthorDtoBase>();
        CreateMap<Author, AuthorDtoFull>();
        // DTO -> Book
        CreateMap<BookDtoBase, Book>();
        CreateMap<BookDtoNew, Book>();
        // Book -> DTO
        CreateMap<Book, BookDtoShort>();
        CreateMap<Book, BookDtoFull>();
        
    }
}