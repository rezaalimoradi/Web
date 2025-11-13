using CMS.Domain.Common;
using CMS.Domain.Users.Entities;
using CMS.Infrastructure.Persistence.Tools;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CMS.Infrastructure.Persistence
{
    /// <summary>
    /// Represents the application database context for interacting with the database.
    /// Inherits from IdentityDbContext with tenant-aware query filtering.
    /// </summary>
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, long>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RegisterEntities(modelBuilder);
            ApplyConfigurations(modelBuilder);
            DefaultEntitySeeder.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        private static void ApplyConfigurations(ModelBuilder modelBuilder)
            => ModelConfigurationHelper.ApplyAllConfigurations(modelBuilder);

        private static void RegisterEntities(ModelBuilder modelBuilder)
        {
            var entityTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName?.StartsWith("CMS") == true && !a.IsDynamic)
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && typeof(BaseEntity).IsAssignableFrom(t));

            foreach (var type in entityTypes)
                modelBuilder.Entity(type);
        }

        #region IUnitOfWork Members

        public new EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
            => base.Entry(entity);

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
            => base.Set<TEntity>();

        public override int SaveChanges()
            => SaveChangesAsync(CancellationToken.None).GetAwaiter().GetResult();

        public async Task<int> SaveChangesAsync()
            => await base.SaveChangesAsync();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
            => await base.SaveChangesAsync(cancellationToken);

        #endregion
    }
}
