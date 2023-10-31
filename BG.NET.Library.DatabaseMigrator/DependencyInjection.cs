using BG.NET.Library.DataAccessLayer.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BG.NET.Library.DatabaseMigrator;

public static class DependencyInjection
{
    public static IServiceCollection ExecuteDatabaseMigrator(this IServiceCollection services, IConfiguration configuration)
    {
        var optionsBuilderData = new DbContextOptionsBuilder<LibraryDbContext>();
        var optionsBuilderIdentity = new DbContextOptionsBuilder<IdentityDbContext>();
        
        optionsBuilderData.UseNpgsql(configuration.GetConnectionString("LibraryData"));
        optionsBuilderIdentity.UseNpgsql(configuration.GetConnectionString("LibraryData"));

        using(var context = new LibraryDbContext(optionsBuilderData.Options))
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
        using(var context = new IdentityDbContext(optionsBuilderIdentity.Options))
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
        return services;
    }
}