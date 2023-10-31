using BG.NET.Library.BusinessLogicLayer.Helpers;
using BG.NET.Library.BusinessLogicLayer.Interfaces;
using BG.NET.Library.BusinessLogicLayer.Services;
using BG.NET.Library.BusinessLogicLayer.Validators.Auth;
using BG.NET.Library.BusinessLogicLayer.Validators.Dto;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BG.NET.Library.BusinessLogicLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
    {
        //Validators (services.AddValidatorsFromAssembly())
        services.AddValidatorsFromAssemblyContaining<LoginUserValidator>();
        services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
        services.AddValidatorsFromAssemblyContaining<BookDtoNewValidator>();
        services.AddValidatorsFromAssemblyContaining<BookDtoUpdateValidator>();
        services.AddValidatorsFromAssemblyContaining<AuthorDtoNewValidator>();
        services.AddValidatorsFromAssemblyContaining<AuthorDtoUpdateValidator>();
        // Services
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IIdentityService, IdentityService>();
        // Mapping
        services.AddAutoMapper(typeof(AutomapperProfile).Assembly);
        return services;
    }
}