using BG.NET.Library.DataAccessLayer.Contexts;
using BG.NET.Library.DataAccessLayer.Interfaces;
using BG.NET.Library.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BG.NET.Library.DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        services.AddDbContext<LibraryDbContext>(options =>
        {
            var dbServer = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost";
            var dbAddress = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "bglibrary";
            var dbUser = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "admin";
            var dbPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "pswd1234";
    
            var connectionString = $"Server={dbServer};" +
                                   $"Database={dbAddress};" +
                                   $"Port=5432;" +
                                   $"User Id={dbUser};" +
                                   $"Password={dbPassword};";
    
            options.UseNpgsql(connectionString);
        });
        // Repositories
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        // Return
        return services;
    }
}