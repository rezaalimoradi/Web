using CMS.Domain.Tenants.Entitis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Tenants
{
    public class WebSiteRoleConfiguration : IEntityTypeConfiguration<WebSiteRole>
    {
        public void Configure(EntityTypeBuilder<WebSiteRole> builder)
        {
            builder.ToTable("WebSiteRoles");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasMany(wr => wr.GroupPermissions)
                  .WithOne(x=> x.WebSiteRole)
                  .HasForeignKey(x => x.WebSiteRoleId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
