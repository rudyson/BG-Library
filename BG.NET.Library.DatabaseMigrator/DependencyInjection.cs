using BG.NET.Library.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BG.NET.Library.DatabaseMigrator;

public static class DependencyInjection
{
    public static IServiceCollection ExecuteDatabaseMigrator(this IServiceCollection services, IConfiguration configuration)
    {
        var optionsBuilderData = new DbContextOptionsBuilder<LibraryDbContext>();
        optionsBuilderData.UseNpgsql(configuration.GetConnectionString("LibraryData"));
        using (var context = new LibraryDbContext(optionsBuilderData.Options))
            context.Database.Migrate();
        return services;
    }
}