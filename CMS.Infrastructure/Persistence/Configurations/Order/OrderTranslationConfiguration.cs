using CMS.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Orders
{
    public class OrderTranslationConfiguration : IEntityTypeConfiguration<OrderTranslation>
    {
        public void Configure(EntityTypeBuilder<OrderTranslation> builder)
        {
            builder.ToTable("OrderTranslations");

            builder.HasKey(x => x.Id);

            // فیلدهای متنی
            builder.Property(x => x.OrderNote)
                .HasMaxLength(1000);

            builder.Property(x => x.ShippingMethod)
                .HasMaxLength(450);

            builder.Property(x => x.PaymentMethod)
                .HasMaxLength(450);

            // روابط
            builder.HasOne(x => x.Order)
                .WithMany(x => x.Translations)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.WebSiteLanguage)
                .WithMany()
                .HasForeignKey(x => x.WebSiteLanguageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
