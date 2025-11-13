using CMS.Domain.Tenants.Entitis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Tenants
{
    public class WebSiteThemeConfiguration : IEntityTypeConfiguration<WebSiteTheme>
    {
        public void Configure(EntityTypeBuilder<WebSiteTheme> builder)
        {
            builder.ToTable("WebSiteThemes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

            builder.Property(x => x.IsDefault)
                   .IsRequired();

            builder.HasOne(x => x.Theme)
                   .WithMany()
                   .HasForeignKey(x => x.ThemeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.WebSite)
                   .WithMany(x => x.Themes)
                   .HasForeignKey(x => x.WebSiteId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.Property(x => x.UpdatedAt)
                   .IsRequired(false);

            builder.HasIndex(x => x.WebSiteId);

            builder.HasIndex(x => x.ThemeId);

            builder.HasIndex(x => new { x.WebSiteId, x.IsDefault })
                   .HasDatabaseName("IX_WebSite_DefaultTheme");
        }
    }
}
