using BG.NET.Library.BusinessLogicLayer.Interfaces;
using BG.NET.Library.BusinessLogicLayer.Services;
using BG.NET.Library.BusinessLogicLayer.Validators;
using BG.NET.Library.Models.Dto.Auth;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BG.NET.Library.BusinessLogicLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        //services.AddValidatorsFromAssembly()
        // Validators
        services.AddScoped<IValidator<LoginDto>, LoginUserValidator>();
        services.AddScoped<IValidator<RegisterDto>, RegisterUserValidator>();
        // Services
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBookService, BookService>();
        return services;
    }
}