using CMS.Domain.Navigation.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Navigation
{
    public class MenuItemTranslationConfiguration : IEntityTypeConfiguration<MenuItemTranslation>
    {
        public void Configure(EntityTypeBuilder<MenuItemTranslation> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(t => t.Link)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.HasOne(t => t.WebSiteLanguage)
                   .WithMany()
                   .HasForeignKey(t => t.WebSiteLanguageId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(t => new { t.MenuItemId, t.WebSiteLanguageId })
                   .IsUnique();

            builder.ToTable("MenuItemTranslations");
        }
    }
}
