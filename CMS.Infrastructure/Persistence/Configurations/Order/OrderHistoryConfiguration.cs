using CMS.Domain.OrderHistories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.OrderHistories
{
    public class OrderHistoryConfiguration : IEntityTypeConfiguration<OrderHistory>
    {
        public void Configure(EntityTypeBuilder<OrderHistory> builder)
        {
            builder.ToTable("OrderHistories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Note)
                .HasMaxLength(1000);

            builder.Property(x => x.EventType)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.Metadata)
                .HasColumnType("nvarchar(max)");

            builder.Property(x => x.CreatedOn)
                .IsRequired();

            builder.HasOne(x => x.Order)
                .WithMany(x => x.Histories)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
