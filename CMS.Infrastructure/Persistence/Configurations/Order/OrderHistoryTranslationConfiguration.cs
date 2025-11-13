using CMS.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Orders
{
    public class OrderHistoryTranslationConfiguration : IEntityTypeConfiguration<OrderHistoryTranslation>
    {
        public void Configure(EntityTypeBuilder<OrderHistoryTranslation> builder)
        {
            builder.ToTable("OrderHistoryTranslations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Note)
                .HasMaxLength(1000);

            builder.Property(x => x.OrderSnapshot)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.HasOne(x => x.OrderHistory)
                .WithMany()
                .HasForeignKey(x => x.OrderHistoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.WebSiteLanguage)
                .WithMany()
                .HasForeignKey(x => x.WebSiteLanguageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
