using CMS.Domain.Discounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Discounts
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discounts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.Value)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.MinimumOrderAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.StartDate);
            builder.Property(x => x.EndDate);

            builder.Property(x => x.MaxUsageCount);
            builder.Property(x => x.UsageCount);

            builder.Property(x => x.IsActive)
                .IsRequired();

            // رابطه با Translation
            builder.HasMany(d => d.Translations)
                .WithOne(t => t.Discount)
                .HasForeignKey(t => t.DiscountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
