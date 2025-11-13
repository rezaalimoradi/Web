using CMS.Domain.Tenants.Entitis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Tenants
{
    public class WebSiteLanguageConfiguration : IEntityTypeConfiguration<WebSiteLanguage>
    {
        public void Configure(EntityTypeBuilder<WebSiteLanguage> builder)
        {
            builder.ToTable("WebSiteLanguages");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(l => l.IsDefault)
                .IsRequired();

            builder.HasIndex(l => new { l.WebSiteId, l.IsDefault })
                .HasFilter("[IsDefault] = 1")
                .IsUnique();
        }
    }
}
