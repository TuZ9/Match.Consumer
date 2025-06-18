using Suitability.Consumer.Application.Services;
using Suitability.Consumer.Application.Services.Messages;
using Suitability.Consumer.Domain.Interfaces.Messages;
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
                .AddScoped<IConsumerService, ConsumerService>()
                .AddScoped<IGatewayService, GatewayService>()
                .AddScoped<ISqsConsumerService, SqsConsumerService>()
                .AddScoped<IReadMessageService, ReadMessageService>();
        }
    }
}
