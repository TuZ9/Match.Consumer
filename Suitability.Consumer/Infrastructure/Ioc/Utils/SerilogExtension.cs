using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Filters;

namespace Suitability.Consumer.Infrastructure.Ioc.Utils
{
    public class SerilogExtension
    {
        public static void AddSerilog(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", $"API Serilog - {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}")
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                .Filter.ByExcluding(z => z.MessageTemplate.Text.Contains("Business error"))
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}