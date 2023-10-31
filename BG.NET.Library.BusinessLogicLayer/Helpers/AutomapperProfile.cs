using AutoMapper;
using BG.NET.Library.Models.Dto.Auth;
using BG.NET.Library.Models.Dto.Library;
using BG.NET.Library.Models.Entities.Auth;
using BG.NET.Library.Models.Entities.Library;

namespace BG.NET.Library.BusinessLogicLayer.Helpers;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        #region Library data
        
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
        CreateMap<Book, BookDtoBase>();
        
        #endregion

        #region Identity

        // Identity
        CreateMap<RegisterDto, User>();
        CreateMap<User, UserInfoDto>();
        
        #endregion
    }
}