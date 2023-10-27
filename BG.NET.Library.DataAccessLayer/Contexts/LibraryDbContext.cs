using BG.NET.Library.Models.Entities.Library;
using Microsoft.EntityFrameworkCore;

namespace BG.NET.Library.DataAccessLayer.Contexts;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options): base (options)
    {
		
    }

    #region DbSets

    public DbSet<Author>? Authors { get; set; }
    public DbSet<Book>? Books { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithOne(b => b.Author);
    }
}