using Microsoft.Extensions.DependencyInjection;
using Suitability.Consumer.Domain.Interfaces.Services;

namespace Suitability.Consumer.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .RegisterServices();
        }

        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IConsumerService, Application.Services.ConsumerService>();
        }
    }
}
