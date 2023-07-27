using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace XmTestTask.Infrastructure.Data
{
    public static class AppDbContextSetUp
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<AppDbContext>(options =>
                options.UseSqlite(connectionString));
        }
    }
}
