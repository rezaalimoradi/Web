using Domain.User.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Persistence.Data;



public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    // اگر نیاز به DbSet برای موجودیت‌های دیگر داری:
    // public DbSet<SomeEntity> SomeEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // مهم! جداول Identity رو می‌سازه

        // تنظیمات اضافی برای AppUser
        modelBuilder.Entity<AppUser>(entity =>
        {
            // کلید اصلی (Guid)
            entity.HasKey(u => u.Id);

            // پیش‌فرض Guid جدید
            entity.Property(u => u.Id).HasDefaultValueSql("NEWID()");

            // تنظیمات فیلدهای اصلی
            entity.Property(u => u.UserName).HasMaxLength(100).IsRequired();
            entity.Property(u => u.Email).HasMaxLength(150).IsRequired();

            // فیلدهای اضافی (اگر در AppUser داری)
            entity.Property(u => u.FirstName).HasMaxLength(50);
            entity.Property(u => u.LastName).HasMaxLength(50);
        });

        // تنظیمات برای AppRole (اختیاری)
        modelBuilder.Entity<AppRole>(entity =>
        {
            entity.Property(r => r.Name).HasMaxLength(100).IsRequired();
        });
        // Seed Roles
        Guid adminRoleId = Guid.Parse("cd2f8322-de2a-45e0-82b0-4edc63076dd8");
        var userRoleId = Guid.Parse("3f638403-30ce-4bec-8f59-ef72c69c8c7c");
        var userId = Guid.Parse("a4b6b311-eb95-4b59-8f09-294f3821b25b");
        modelBuilder.Entity<AppRole>().HasData(
            new AppRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa" // Static!
            },
            new AppRole
            {
                Id = userRoleId,
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" 
            }
        );

        modelBuilder.Entity<AppUser>().HasData(
            new AppUser
            {
                Id = userId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEKyIYbOPoj+cbmlaapPR9GHPTvqf6wRINg33+qGXwZvRC4Wy53q1P6gbwo6oO8NhqQ==", 
                SecurityStamp = "cccccccc-cccc-cccc-cccc-cccccccccccc", // Static!
                ConcurrencyStamp = "dddddddd-dddd-dddd-dddd-dddddddddddd", // Static!
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0
            }
        );

        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
            new IdentityUserRole<Guid>
            {
                UserId = userId,
                RoleId = adminRoleId
            }
        );
    }
}
