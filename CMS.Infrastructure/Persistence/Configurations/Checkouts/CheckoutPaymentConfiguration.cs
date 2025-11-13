using CMS.Domain.Checkouts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Checkouts
{
    public class CheckoutPaymentConfiguration : IEntityTypeConfiguration<CheckoutPayment>
    {
        public void Configure(EntityTypeBuilder<CheckoutPayment> builder)
        {
            builder.ToTable("CheckoutPayments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.PaymentFee)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.PaymentMethod)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.GatewayTransactionId)
                .HasMaxLength(450);

            builder.Property(x => x.FailureMessage)
                .HasMaxLength(1000);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.CreatedOn)
                .IsRequired();

            builder.Property(x => x.LatestUpdatedOn)
                .IsRequired();

            builder.HasOne(x => x.Checkout)
                .WithMany() // اگه توی Checkout کلکشن Payments داری اینجا باید WithMany(p => p.Payments) باشه
                .HasForeignKey(x => x.CheckoutId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
