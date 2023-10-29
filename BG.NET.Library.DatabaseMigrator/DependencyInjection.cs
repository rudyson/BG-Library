using BG.NET.Library.DataAccessLayer.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BG.NET.Library.DatabaseMigrator;

public static class DependencyInjection
{
    public static IServiceCollection ExecuteDatabaseMigrator(this IServiceCollection services, IConfiguration configuration)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("LibraryData"));

        using(var context = new LibraryDbContext(optionsBuilder.Options))
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
        return services;
    }
}