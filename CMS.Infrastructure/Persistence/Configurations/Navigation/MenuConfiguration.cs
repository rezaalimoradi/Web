using CMS.Domain.Navigation.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Navigation
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(m => m.WebSite)
                   .WithMany()
                   .HasForeignKey(m => m.WebSiteId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(m => m.Items)
                   .WithOne(i => i.Menu)
                   .HasForeignKey(i => i.MenuId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Menus");
        }
    }
}
