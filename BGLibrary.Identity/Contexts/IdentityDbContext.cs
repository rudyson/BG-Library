using BG.NET.Library.Models.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace BGLibrary.Identity.Contexts;

public class IdentityDbContext: DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options): base (options)
    {
		
    }
    public DbSet<User>? Users { get; set; }
}