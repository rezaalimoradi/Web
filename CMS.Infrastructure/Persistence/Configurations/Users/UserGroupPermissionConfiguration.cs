using CMS.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CMS.Infrastructure.Persistence.Configurations.Users
{
    public class UserGroupPermissionConfiguration : IEntityTypeConfiguration<UserGroupPermission>
    {
        public void Configure(EntityTypeBuilder<UserGroupPermission> builder)
        {
            builder.ToTable("UserGroupPermissions");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                   .WithMany(u => u.UserGroupPermissions)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.GroupPermission)
                   .WithMany(g => g.UserGroupPermissions)
                   .HasForeignKey(x => x.GroupPermissionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.UserId, x.GroupPermissionId }).IsUnique();
        }
    }
}
