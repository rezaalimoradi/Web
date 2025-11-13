using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using System.Reflection;

namespace Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection AddPersistenceServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // ✅ رجیستر DbContext
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // ✅ AutoMapper در لایه Persistence معمولاً نیازی نیست
            // چون این لایه فقط مسئول ارتباط با دیتابیسه.
            // اما اگر واقعاً مپینگ‌ خاصی در این لایه داری، می‌تونی نگه داری.
            // در غیر اینصورت پیشنهاد میشه این خط حذف بشه و فقط در Application استفاده شه.
            // services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
