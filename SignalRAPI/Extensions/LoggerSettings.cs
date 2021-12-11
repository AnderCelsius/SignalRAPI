using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace SignalRAPI.Extensions
{
    public static class LoggerSettings
    {
        public static void SetupSerilog(IConfiguration config)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(
                    path: ".\\Logs\\log-.txt",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Information
                )
                .CreateLogger();
        }
    }
}
