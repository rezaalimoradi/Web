using CMS.Domain.Orders.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Order
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            // 🔹 کلید اصلی
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .ValueGeneratedOnAdd();

            // 🔹 CustomerIdentifier
            builder.Property(c => c.CustomerIdentifier)
                   .IsRequired()
                   .HasMaxLength(256);

            // 🔹 تاریخ‌ها
            builder.Property(c => c.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()"); // مقدار پیش‌فرض سرور

            builder.Property(c => c.UpdatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            // 🔹 رابطه با WebSite
            builder.HasOne(c => c.WebSite)
                   .WithMany()
                   .HasForeignKey(c => c.WebSiteId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            // 🔹 رابطه با CartItem
            builder.HasMany(c => c.Items)
                   .WithOne(i => i.Cart)
                   .HasForeignKey(i => i.CartId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 🔹 رابطه با CartTranslation
            builder.HasMany(c => c.Translations)
                   .WithOne(t => t.Cart)
                   .HasForeignKey(t => t.CartId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 🔹 ایندکس ترکیبی (برای جستجوی سریع سبد یک مشتری در یک سایت)
            builder.HasIndex(c => new { c.WebSiteId, c.CustomerIdentifier })
                   .IsUnique(false)
                   .HasDatabaseName("IX_Carts_WebSite_Customer");

            // 🔹 برای اطمینان از داده‌های سالم
            builder.Navigation(c => c.Items).AutoInclude(false);
            builder.Navigation(c => c.Translations).AutoInclude(false);
        }
    }
}
