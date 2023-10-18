using BGLibrary.Identity.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BGLibrary.Identity.Contexts;

public class IdentityDbContext: DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options): base (options)
    {
		
    }
    public DbSet<User>? Users { get; set; }
}