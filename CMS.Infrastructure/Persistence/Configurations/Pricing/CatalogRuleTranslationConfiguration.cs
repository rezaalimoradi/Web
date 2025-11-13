using CMS.Domain.Pricing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Pricing
{
    public class CatalogRuleTranslationConfiguration : IEntityTypeConfiguration<CatalogRuleTranslation>
    {
        public void Configure(EntityTypeBuilder<CatalogRuleTranslation> builder)
        {
            builder.ToTable("CatalogRuleTranslations");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Culture)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(t => t.Name)
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(t => t.Description)
                   .HasMaxLength(1000);

            builder.HasOne(t => t.CatalogRule)
                   .WithMany(r => r.Translations)
                   .HasForeignKey(t => t.CatalogRuleId)
                   .OnDelete(DeleteBehavior.Cascade);

            // یکتا بودن ترجمه بر اساس CatalogRule + Culture
            builder.HasIndex(t => new { t.CatalogRuleId, t.Culture })
                   .IsUnique();
        }
    }
}
