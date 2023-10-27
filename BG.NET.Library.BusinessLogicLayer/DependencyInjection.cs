using BG.NET.Library.BusinessLogicLayer.Validators;
using BG.NET.Library.Models.Dto.Auth;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BG.NET.Library.BusinessLogicLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        //services.AddValidatorsFromAssembly()
        services.AddScoped<IValidator<LoginDto>, LoginUserValidator>();
        services.AddScoped<IValidator<RegisterDto>, RegisterUserValidator>();
        return services;
    }
}