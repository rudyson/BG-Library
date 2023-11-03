using BGNet.TestAssignment.BusinessLogic.Helpers;
using BGNet.TestAssignment.BusinessLogic.Interfaces.Auth;
using BGNet.TestAssignment.BusinessLogic.Interfaces.Library;
using BGNet.TestAssignment.BusinessLogic.Services.Auth;
using BGNet.TestAssignment.BusinessLogic.Services.Library;
using BGNet.TestAssignment.DataAccess;
using BGNet.TestAssignment.DatabaseMigrator;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BGNet.TestAssignment.BusinessLogic;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
    {
        // Validators
        services.AddValidatorsFromAssemblyContaining<AuthorService>();

        // Services
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IIdentityService, IdentityService>();

        // Mapping
        services.AddMapster();
        MapsterMapperRegistration.Init();

        // Dependency Injection
        services.AddDataAccessLayer(configuration);
        services.ExecuteDatabaseMigrator(configuration);

        return services;
    }
}