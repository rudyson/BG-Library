using AutoMapper;
using BGLibrary.Identity.Models.Dto;
using BGLibrary.Identity.Models.Entities;

namespace BGLibrary.Identity.Tools;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<RegisterDto, User>();
    }
}