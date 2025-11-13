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
    public class WishlistItemConfiguration : IEntityTypeConfiguration<WishlistItem>
    {
        public void Configure(EntityTypeBuilder<WishlistItem> builder)
        {
            builder.ToTable("WishlistItems");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.ProductId)
                   .IsRequired();

            builder.Property(i => i.WishlistId)
                   .IsRequired();

            builder.Property(i => i.AddedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            // ✅ جلوگیری از تکراری شدن آیتم در یک Wishlist
            builder.HasIndex(i => new { i.WishlistId, i.ProductId })
                   .IsUnique();

            builder.HasOne(i => i.Wishlist)
                   .WithMany(w => w.Items)
                   .HasForeignKey(i => i.WishlistId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Product)
                   .WithMany()
                   .HasForeignKey(i => i.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
