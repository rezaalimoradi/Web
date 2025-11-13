using CMS.Domain.Wishlist.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Persistence.Configurations.Wishlists
{
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.ToTable("Wishlists");

            builder.HasKey(w => w.Id);

            builder.Property(cl => cl.WebsiteId)
       .IsRequired();

            builder.Property(w => w.CustomerId)
                   .IsRequired();

            builder.Property(w => w.CreatedAt)
                   .IsRequired();

            builder.Property(w => w.UpdatedAt)
                   .IsRequired();

            // ✅ یک Wishlist چند آیتم دارد
            builder.HasMany(w => w.Items)
                   .WithOne(i => i.Wishlist)
                   .HasForeignKey(i => i.WishlistId)
                   .OnDelete(DeleteBehavior.Cascade); // حذف لیست → حذف آیتم‌ها

            // ✅ Index پیشنهادی برای سرعت
            builder.HasIndex(cl => new { cl.CustomerId, cl.WebsiteId });
        }
    }
}
