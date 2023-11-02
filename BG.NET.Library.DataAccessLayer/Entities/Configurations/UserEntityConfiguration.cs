using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BG.NET.Library.DataAccess.Entities.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Username).IsRequired();
            builder.Property(e => e.Password).IsRequired();
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Surname).IsRequired();
            builder.Property(e => e.Address).IsRequired();
            builder.Property(e => e.Birthday).IsRequired();
        }
    }
}
