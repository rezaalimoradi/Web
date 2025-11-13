using CMS.Domain.Discounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Discounts
{
    public class DiscountTranslationConfiguration : IEntityTypeConfiguration<DiscountTranslation>
    {
        public void Configure(EntityTypeBuilder<DiscountTranslation> builder)
        {
            builder.ToTable("DiscountTranslations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.WebSiteLanguageId)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            // رابطه با Discount
            builder.HasOne(x => x.Discount)
                .WithMany(d => d.Translations)
                .HasForeignKey(x => x.DiscountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
