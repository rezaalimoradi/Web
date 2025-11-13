using CMS.Domain.Pricing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Pricing
{
    public class CatalogRuleConfiguration : IEntityTypeConfiguration<CatalogRule>
    {
        public void Configure(EntityTypeBuilder<CatalogRule> builder)
        {
            builder.ToTable("CatalogRules");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(r => r.Description)
                   .HasMaxLength(1000);

            builder.Property(r => r.IsActive)
                   .IsRequired();

            builder.Property(r => r.CreatedOn)
                   .IsRequired();

            builder.Property(r => r.LatestUpdatedOn)
                   .IsRequired();

            // رابطه با Translation
            builder.HasMany(r => r.Translations)
                   .WithOne(t => t.CatalogRule)
                   .HasForeignKey(t => t.CatalogRuleId)
                   .OnDelete(DeleteBehavior.Cascade);

            // اندیس روی Name برای سریع‌تر شدن سرچ‌ها
            builder.HasIndex(r => r.Name);
        }
    }
}
