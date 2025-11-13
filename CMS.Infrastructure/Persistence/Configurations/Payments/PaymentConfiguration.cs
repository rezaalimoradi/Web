using CMS.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Payments
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PaymentMethod)
                   .HasMaxLength(450)
                   .IsRequired();

            builder.Property(x => x.GatewayTransactionId)
                   .HasMaxLength(450);

            builder.Property(x => x.Amount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.PaymentFee)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.FailureMessage)
                   .HasMaxLength(1000);

            builder.Property(x => x.Status)
                   .IsRequired();

            builder.Property(x => x.CreatedOn)
                   .IsRequired();

            builder.Property(x => x.LatestUpdatedOn)
                   .IsRequired();

            builder.HasOne(x => x.Order)
                   .WithMany()
                   .HasForeignKey(x => x.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Translations)
                   .WithOne(t => t.Payment)
                   .HasForeignKey(t => t.PaymentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
