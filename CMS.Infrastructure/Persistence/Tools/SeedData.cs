using CMS.Domain.Constants;
using CMS.Domain.Localization.Entities;
using CMS.Domain.Tenants.Entitis;
using CMS.Domain.Themes.Entities;
using CMS.Domain.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Persistence.Tools
{
    /// <summary>
    /// Provides default data seeding for the application's data model.
    /// </summary>
    internal static class DefaultEntitySeeder
    {
        private static DateTime seedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Seeds all default data into the provided model builder.
        /// Call this method from OnModelCreating in your DbContext.
        /// </summary>
        /// <param name="modelBuilder">EF Core model builder</param>
        internal static void Seed(ModelBuilder modelBuilder)
        {
            SeedDomainExtensions(modelBuilder);

            SeedWebSite(modelBuilder);

            SeedTheme(modelBuilder);

            SeedDomain(modelBuilder);

            SeedRoles(modelBuilder);

            SeedUser(modelBuilder);

            SeedWebSiteRole(modelBuilder);

            SeedUserRoles(modelBuilder);

            SeedLanguages(modelBuilder);
        }

        private static void SeedDomain(ModelBuilder modelBuilder)
        {
            var weboneDomain = new WebSiteDomain()
            {
                Id = 1,
                CreatedAt = seedDate,
                DomainName = "hicommerce",
                IsDefault = true,
                TldId = 2,
                WebSiteId = 1
            };

            modelBuilder.Entity<WebSiteDomain>().HasData(weboneDomain);
        }

        private static void SeedWebSite(ModelBuilder modelBuilder)
        {
            var contactInfo = new Domain.Tenants.ValueObjects.ContactInfo("info@hicommerce.ir", "09360735388", "");

            var weboneSite = new WebSite()
            {
                Id = 1,
                //ContactInfo = contactInfo,
                CompanyName = "hicommerce",
                IsActive = true,
                CreatedAt = seedDate
            };

            modelBuilder.Entity<WebSite>().HasData(weboneSite);
        }

        private static void SeedTheme(ModelBuilder modelBuilder)
        {
            var theme = new Theme()
            {
                Id = 1,
                Name = "hicommerce",
                CreatedAt = seedDate
            };
            modelBuilder.Entity<Theme>().HasData(theme);

            var websiteTheme = new WebSiteTheme(true, 1, 1)
            {
                Id = 1
            };
            modelBuilder.Entity<WebSiteTheme>().HasData(websiteTheme);
        }

        private static void SeedWebSiteRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WebSiteRole>().HasData(
                new WebSiteRole { Id = 1, CreatedAt = seedDate, RoleId = 1, WebSiteId = 1 },
                new WebSiteRole { Id = 2, CreatedAt = seedDate, RoleId = 2, WebSiteId = 1 },
                new WebSiteRole { Id = 3, CreatedAt = seedDate, RoleId = 3, WebSiteId = 1 }
                );
        }

        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppRole>(x =>
            {

                x.HasData(new AppRole { Id = 1, Name = RoleConsts.Admin, NormalizedName = RoleConsts.Admin.ToUpper() });

                x.HasData(new AppRole { Id = 2, Name = RoleConsts.User, NormalizedName = RoleConsts.User.ToUpper() });

                x.HasData(new AppRole { Id = 3, Name = RoleConsts.Developer, NormalizedName = RoleConsts.Developer.ToUpper() });

            });
        }

        private static void SeedUser(ModelBuilder modelBuilder)
        {
            var passwordHasher = new PasswordHasher<AppUser>();
            var adminUser = new AppUser
            {
                Id = 1,
                UserName = RoleConsts.Developer,
                NormalizedUserName = RoleConsts.Developer.ToUpper(),
                Email = "razani.mohammad1@gmail.com",
                NormalizedEmail = "RAZANI.MOHAMMAD1@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = "f2b5b3b4-4a65-4d4d-a7d4-305f7a1e4f3e",
                WebSiteId = 1,
                IsActive = true,
                PhoneNumber = "09360735388",
                PhoneNumberConfirmed = true,
                AccessFailedCount = 0,
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                ConcurrencyStamp = "a8110c5e-04e0-4d5b-9c3a-3f3c594ed8d6",
            };
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Test@123");

            modelBuilder.Entity<AppUser>().HasData(adminUser);
        }

        private static void SeedUserRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<long>>().HasData(
                new IdentityUserRole<long> { RoleId = 1, UserId = 1 },
                new IdentityUserRole<long> { RoleId = 2, UserId = 1 },
                new IdentityUserRole<long> { RoleId = 3, UserId = 1 }
            );
        }

        private static void SeedDomainExtensions(ModelBuilder modelBuilder)
        {
            int idCounter = 1;
            modelBuilder.Entity<TopLevelDomain>(x =>
            {
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".com", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".ir", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".nl", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".be", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".de", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".eu", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".fr", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".net", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".info", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".org", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".biz", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".couk", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".nu", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".tv", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".co", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".me", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".us", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".in", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".ca", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".com.co", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".net.in", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".ws", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".se", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".pro", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".ac.ir", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".shop", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".co.ir", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".com.au", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".institute", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".co.com", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".net.co", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".bio", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".love", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".online", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".com.tr", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".graphics", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".it", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".sch.ir", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".ae", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".holdings", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".om", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".click", CreatedAt = seedDate });
                x.HasData(new TopLevelDomain { Id = idCounter++, Extension = ".vip", CreatedAt = seedDate });
            });
        }

        private static void SeedLanguages(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>(x =>
            {
                x.HasData(
                    new Language { Id = 1, Code = "fa", Name = "Persian", CreatedAt = seedDate, UpdatedAt = null },
                    new Language { Id = 2, Code = "en", Name = "English", CreatedAt = seedDate, UpdatedAt = null },
                    new Language { Id = 3, Code = "es", Name = "Spanish", CreatedAt = seedDate, UpdatedAt = null },
                    new Language { Id = 4, Code = "fr", Name = "French", CreatedAt = seedDate, UpdatedAt = null },
                    new Language { Id = 5, Code = "de", Name = "German", CreatedAt = seedDate, UpdatedAt = null },
                    new Language { Id = 6, Code = "zh", Name = "Chinese (Mandarin)", CreatedAt = seedDate, UpdatedAt = null },
                    new Language { Id = 7, Code = "ar", Name = "Arabic", CreatedAt = seedDate, UpdatedAt = null },
                    new Language { Id = 8, Code = "ru", Name = "Russian", CreatedAt = seedDate, UpdatedAt = null },
                    new Language { Id = 9, Code = "ja", Name = "Japanese", CreatedAt = seedDate, UpdatedAt = null },
                    new Language { Id = 10, Code = "pt", Name = "Portuguese", CreatedAt = seedDate, UpdatedAt = null },
                    new Language { Id = 11, Code = "hi", Name = "Hindi", CreatedAt = seedDate, UpdatedAt = null },
                    new Language { Id = 12, Code = "it", Name = "Italian", CreatedAt = seedDate, UpdatedAt = null },
                    new Language { Id = 13, Code = "ko", Name = "Korean", CreatedAt = seedDate, UpdatedAt = null },
                    new Language { Id = 14, Code = "nl", Name = "Dutch", CreatedAt = seedDate, UpdatedAt = null },
                    new Language { Id = 15, Code = "sv", Name = "Swedish", CreatedAt = seedDate, UpdatedAt = null },
                    new Language { Id = 16, Code = "no", Name = "Norwegian", CreatedAt = seedDate, UpdatedAt = null },
                    new Language { Id = 17, Code = "da", Name = "Danish", CreatedAt = seedDate, UpdatedAt = null },
                    new Language { Id = 18, Code = "fi", Name = "Finnish", CreatedAt = seedDate, UpdatedAt = null },
                    new Language { Id = 19, Code = "tr", Name = "Turkish", CreatedAt = seedDate, UpdatedAt = null },
                    new Language { Id = 20, Code = "pl", Name = "Polish", CreatedAt = seedDate, UpdatedAt = null },
                    new Language { Id = 21, Code = "he", Name = "Hebrew", CreatedAt = seedDate, UpdatedAt = null }
                );
            });

            modelBuilder.Entity<WebSiteLanguage>(x =>
            {
                x.HasData(new WebSiteLanguage() {Id =1, CreatedAt = seedDate, IsDefault = true, LanguageId = 1, WebSiteId = 1 });
            });
        }

    }
}
