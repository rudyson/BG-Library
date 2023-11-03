using BGNet.TestAssignment.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BGNet.TestAssignment.DatabaseMigrator;

public static class DependencyInjection
{
    public static IServiceCollection ExecuteDatabaseMigrator(this IServiceCollection services, IConfiguration configuration)
    {
        var optionsBuilderData = new DbContextOptionsBuilder<LibraryDbContext>();
        optionsBuilderData.UseNpgsql(configuration.GetConnectionString("LibraryData"));

        using (var context = new LibraryDbContext(optionsBuilderData.Options))
        {
            try
            {
                context.Database.Migrate();
            }
            catch (Exception)
            {
                Environment.Exit(5432);
            }
		}
            
        return services;
    }
}