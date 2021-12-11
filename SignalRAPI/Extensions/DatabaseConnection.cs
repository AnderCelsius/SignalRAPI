using SignalRAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SignalRAPI.Extensions
{
    public static class DatabaseConnection
    {
        public static void AddDbContextAndConfigurations(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContextPool<AppDbContext>(options =>
            {
                string connStr = config.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connStr).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }
    }
}