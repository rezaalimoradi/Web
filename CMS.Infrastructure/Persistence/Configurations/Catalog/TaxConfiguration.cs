using CMS.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Catalog
{
    public class TaxConfiguration : IEntityTypeConfiguration<Tax>
    {
        public void Configure(EntityTypeBuilder<Tax> builder)
        {
            builder.ToTable("Taxs");


            builder.HasKey(p => p.Id);

            builder.Property(t => t.Rate);

            builder.Property(t => t.IsActive)
                .IsRequired(false)
                .HasDefaultValue(false);

            builder.HasMany(c => c.ProductTaxs)
                .WithOne(t => t.Tax)
                .HasForeignKey(t => t.TaxId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.Translations)
                .WithOne(t => t.Tax)
                .HasForeignKey(t => t.TaxId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
