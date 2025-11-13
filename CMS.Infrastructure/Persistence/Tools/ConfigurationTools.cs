using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Persistence.Tools
{
    /// <summary>
    /// Provides helper methods for applying EF Core model configurations.
    /// </summary>
    public static class ModelConfigurationHelper
    {
        /// <summary>
        /// Loads and applies all entity configurations found in the same assembly as the DbContext.
        /// </summary>
        /// <param name="builder">The EF Core ModelBuilder instance.</param>
        public static void ApplyAllConfigurations(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
