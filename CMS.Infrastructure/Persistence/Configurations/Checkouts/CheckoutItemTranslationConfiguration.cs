using CMS.Domain.Checkouts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Checkouts
{
    public class CheckoutItemTranslationConfiguration : IEntityTypeConfiguration<CheckoutItemTranslation>
    {
        public void Configure(EntityTypeBuilder<CheckoutItemTranslation> builder)
        {
            builder.ToTable("CheckoutItemTranslations");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.ProductName)
                   .IsRequired()
                   .HasMaxLength(450);

            builder.HasIndex(t => new { t.CheckoutItemId, t.WebSiteLanguageId })
                   .IsUnique(); // هر آیتم یک ترجمه در هر زبان

            builder.HasOne(t => t.CheckoutItem)
                   .WithMany(i => i.Translations)
                   .HasForeignKey(t => t.CheckoutItemId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
