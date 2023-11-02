using BG.NET.Library.BusinessLogic.Helpers;
using BG.NET.Library.BusinessLogic.Interfaces;
using BG.NET.Library.BusinessLogic.Services;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BG.NET.Library.BusinessLogic;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
    {
        //Validators
        services.AddValidatorsFromAssemblyContaining<AuthorService>();
        // Services
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IIdentityService, IdentityService>();
        // Mapping
        services.AddMapster();
        MapsterMapperRegistration.Init();
        return services;
    }
}