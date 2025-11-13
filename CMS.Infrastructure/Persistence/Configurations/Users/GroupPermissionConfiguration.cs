using CMS.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CMS.Infrastructure.Persistence.Configurations.Users
{
    public class GroupPermissionConfiguration : IEntityTypeConfiguration<GroupPermission>
    {
        public void Configure(EntityTypeBuilder<GroupPermission> builder)
        {
            builder.ToTable("GroupPermissions");

            builder.HasKey(gp => gp.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(gp => gp.Name).IsRequired();

            builder.HasMany(gp => gp.Permissions)
                   .WithMany()
                   .UsingEntity<Dictionary<string, object>>(
                       "GroupPermissionPermission",
                       r => r.HasOne<Permission>()
                             .WithMany()
                             .HasForeignKey("PermissionId")
                             .OnDelete(DeleteBehavior.NoAction),
                       l => l.HasOne<GroupPermission>()
                             .WithMany()
                             .HasForeignKey("GroupPermissionId")
                             .OnDelete(DeleteBehavior.NoAction), 
                       je =>
                       {
                           je.HasKey("GroupPermissionId", "PermissionId");
                           je.ToTable("GroupPermissionPermissions");
                       });

        }
    }
}
