using AutoMapper;
using BG.NET.Library.Models.Dto.Auth;
using BG.NET.Library.Models.Entities.Auth;

namespace BGLibrary.Identity.Tools;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<RegisterDto, User>();
    }
}