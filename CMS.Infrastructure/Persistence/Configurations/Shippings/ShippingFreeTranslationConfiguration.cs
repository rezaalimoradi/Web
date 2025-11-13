using CMS.Domain.Shippings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Shippings
{
    public class ShippingFreeTranslationConfiguration : IEntityTypeConfiguration<ShippingFreeTranslation>
    {
        public void Configure(EntityTypeBuilder<ShippingFreeTranslation> builder)
        {
            builder.ToTable("ShippingFreeTranslations");

            builder.HasKey(sft => sft.Id);

            // Relations
            builder.HasOne(sft => sft.ShippingFree)
                   .WithMany()
                   .HasForeignKey(sft => sft.ShippingFreeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sft => sft.WebSiteLanguage)
                   .WithMany()
                   .HasForeignKey(sft => sft.WebSiteLanguageId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Properties
            builder.Property(sft => sft.Name)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(sft => sft.Description)
                   .HasMaxLength(1000);
        }
    }
}
