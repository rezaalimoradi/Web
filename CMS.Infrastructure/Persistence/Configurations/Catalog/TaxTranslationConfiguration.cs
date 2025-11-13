using CMS.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Catalog
{

    public class TaxTranslationConfiguration : IEntityTypeConfiguration<TaxTranslation>
    {
        public void Configure(EntityTypeBuilder<TaxTranslation> builder)
        {
            builder.ToTable("TaxTranslations");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(t => t.Description)
                .HasMaxLength(2000);

            builder.HasOne(x => x.WebSiteLanguage)
                .WithMany()
                .HasForeignKey(x => x.WebSiteLanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(t => new { t.TaxId, t.WebSiteLanguageId })
                .IsUnique();
        }
    }
}
