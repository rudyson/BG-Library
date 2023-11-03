using BGNet.TestAssignment.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BGNet.TestAssignment.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LibraryDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("LibraryData"));
        });
        return services;
    }
}