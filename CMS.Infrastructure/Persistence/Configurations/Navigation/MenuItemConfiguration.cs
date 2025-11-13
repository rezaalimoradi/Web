using CMS.Domain.Navigation.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Navigation
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasKey(mi => mi.Id);
            builder.Property(mi => mi.Id).ValueGeneratedOnAdd();

            builder.Property(mi => mi.IsActive).IsRequired();
            builder.Property(mi => mi.DisplayOrder).IsRequired();

            builder.HasOne(mi => mi.Parent)
                   .WithMany(p => p.Children)
                   .HasForeignKey(mi => mi.ParentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(mi => mi.Translations)
                   .WithOne(t => t.MenuItem)
                   .HasForeignKey(t => t.MenuItemId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("MenuItems");
        }
    }
}
