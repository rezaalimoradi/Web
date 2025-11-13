using CMS.Domain.Orders.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Order
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");

            // 🔹 کلید اصلی
            builder.HasKey(ci => ci.Id);
            builder.Property(ci => ci.Id)
                   .ValueGeneratedOnAdd();

            // 🔹 ویژگی‌ها
            builder.Property(ci => ci.Quantity)
                   .IsRequired();

            builder.Property(ci => ci.UnitPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            builder.Property(ci => ci.Discount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            // 🔹 روابط

            // هر CartItem متعلق به یک Cart است
            builder.HasOne(ci => ci.Cart)
                   .WithMany(c => c.Items)
                   .HasForeignKey(ci => ci.CartId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            // هر CartItem مربوط به یک Product است
            builder.HasOne(ci => ci.Product)
                   .WithMany()
                   .HasForeignKey(ci => ci.ProductId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            // هر CartItem می‌تواند چند ترجمه داشته باشد
            builder.HasMany(ci => ci.Translations)
                   .WithOne(t => t.CartItem)
                   .HasForeignKey(t => t.CartItemId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 🔹 ایندکس ترکیبی برای اطمینان از یکتا بودن محصول در هر Cart
            builder.HasIndex(ci => new { ci.CartId, ci.ProductId })
                   .IsUnique()
                   .HasDatabaseName("IX_CartItems_Cart_Product");

            // 🔹 جلوگیری از بارگذاری خودکار برای بهبود عملکرد
            builder.Navigation(ci => ci.Translations).AutoInclude(false);
        }
    }
}
