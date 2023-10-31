using BG.NET.Library.DataAccessLayer.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BG.NET.Library.DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LibraryDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("LibraryData"), 
                b => b.MigrationsAssembly("BG.NET.Library.API.Data")
                );
        });
        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("LibraryData"),
                b => b.MigrationsAssembly("BG.NET.Library.API.Identity")
                );
        });
        return services;
    }
}