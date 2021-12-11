
using Microsoft.Extensions.DependencyInjection;
using SignalRAPI.Core.Implementations;
using SignalRAPI.Core.Interfaces;
using SignalRAPI.Data.UnitOfWork;

namespace SignalRAPI.Extensions
{
    public static class DIServices
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRequestFormService, RequestFormService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
