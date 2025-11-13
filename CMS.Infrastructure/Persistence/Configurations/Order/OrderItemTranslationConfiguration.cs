using CMS.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Orders
{
    public class OrderItemTranslationConfiguration : IEntityTypeConfiguration<OrderItemTranslation>
    {
        public void Configure(EntityTypeBuilder<OrderItemTranslation> builder)
        {
            builder.ToTable("OrderItemTranslations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductName)
                .HasMaxLength(450)
                .IsRequired();

            builder.Property(x => x.ProductDescription)
                .HasMaxLength(2000);

            // روابط
            builder.HasOne(x => x.OrderItem)
                .WithMany()
                .HasForeignKey(x => x.OrderItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.WebSiteLanguage)
                .WithMany()
                .HasForeignKey(x => x.WebSiteLanguageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
