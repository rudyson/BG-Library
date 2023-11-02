using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BG.NET.Library.DataAccess.Entities.Configurations
{
    public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Surname).IsRequired();
            builder.Property(x => x.Birthday).IsRequired();

            builder
                .HasMany(x => x.Books)
                .WithOne(x => x.Author)
                .HasForeignKey(x => x.Id);
        }
    }
}
