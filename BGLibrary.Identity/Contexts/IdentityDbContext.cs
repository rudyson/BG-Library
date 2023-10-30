using BG.NET.Library.Models.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace BGLibrary.Identity.Contexts;

public class IdentityDbContext: DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options): base (options)
    {
		
    }
    public DbSet<User>? Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(e => e.Id);
        modelBuilder.Entity<User>().Property(e => e.Username).IsRequired();
        modelBuilder.Entity<User>().Property(e => e.Password).IsRequired();
        modelBuilder.Entity<User>().Property(e => e.Name).IsRequired();
        modelBuilder.Entity<User>().Property(e => e.Surname).IsRequired();
        modelBuilder.Entity<User>().Property(e => e.Address).IsRequired();
        modelBuilder.Entity<User>().Property(e => e.Birthday).IsRequired();
    }
}