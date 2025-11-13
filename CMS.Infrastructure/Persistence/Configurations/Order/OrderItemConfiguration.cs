using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductPrice)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.Property(x => x.Quantity)
               .IsRequired();

        builder.Property(x => x.DiscountAmount)
               .HasColumnType("decimal(18,2)")
               .HasDefaultValue(0m);

        builder.Property(x => x.TaxAmount)
               .HasColumnType("decimal(18,2)")
               .HasDefaultValue(0m);

        builder.Property(x => x.TaxPercent)
               .HasColumnType("decimal(5,2)")
               .HasDefaultValue(0m);

        // Foreign key صحیح به Product
        builder.HasOne(x => x.Product)
               .WithMany(p => p.OrderItems)
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }

}
