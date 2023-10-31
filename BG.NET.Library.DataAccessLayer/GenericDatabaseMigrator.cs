using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BG.NET.Library.DataAccessLayer;
/*
public class GenericDatabaseMigrator<TDbContext> where TDbContext : DbContext, new()
{
    public void ExecuteMigrationAssistant(IConfiguration configuration)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();

        optionsBuilder.UseNpgsql(configuration.GetConnectionString("LibraryData"));

        using(var context = new TDbContext(optionsBuilder.Options))
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
    }
}
*/