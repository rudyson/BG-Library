using AutoMapper;
using BGLibrary.Library.Models.Dto;
using BGLibrary.Library.Models.Entities;

namespace BGLibrary.Library.Tools;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<NewBookDto, Book>();
        CreateMap<UpdateBookDto, Book>();
        CreateMap<NewAuthorDto, Author>();
    }
}