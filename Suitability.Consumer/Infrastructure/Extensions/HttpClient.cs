using Suitability.Consumer.Domain.Interfaces.ApiClientService;
using Suitability.Consumer.Infrastructure.HttpClientBase;
using Suitability.Consumer.Infrastructure.Static;


namespace Suitability.Consumer.Infrastructure.Extensions
{
    public static class HttpClient
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            var accountLogger = serviceProvider.GetService(typeof(ILogger<AccountApiClient>)) as ILogger<AccountApiClient>;
            var productLogger = serviceProvider.GetService(typeof(ILogger<ProductApiClient>)) as ILogger<ProductApiClient>;
            var complementLogger = serviceProvider.GetService(typeof(ILogger<ProductComplementApiClient>)) as ILogger<ProductComplementApiClient>;
            var strainLogger = serviceProvider.GetService(typeof(ILogger<DocumentStatusApiClient>)) as ILogger<DocumentStatusApiClient>;

            services.AddHttpClient("Account", client => { client.BaseAddress = new Uri(RunTimeConfig.SuitabilityProductEndpoint); });
            services.AddHttpClient("Product", client => { client.BaseAddress = new Uri(RunTimeConfig.SuitabilityProductEndpoint); });
            services.AddHttpClient("ProductComplement", client => { client.BaseAddress = new Uri(RunTimeConfig.SuitabilityProductComplementEndpoint); });
            services.AddHttpClient("DocumentStatus", client => { client.BaseAddress = new Uri(RunTimeConfig.SuitabilityProductEndpoint); });

            services.AddSingleton<IAccountApiClient, AccountApiClient>(x => new AccountApiClient(x.GetService<IHttpClientFactory>()!, accountLogger, "Account"));
            services.AddSingleton<IProductApiClient, ProductApiClient>(x => new ProductApiClient(x.GetService<IHttpClientFactory>()!, productLogger, "Product"));
            services.AddSingleton<IProductComplementApiClient, ProductComplementApiClient>(x => new ProductComplementApiClient(x.GetService<IHttpClientFactory>()!, complementLogger, "ProductComplement"));
            services.AddSingleton<IDocumentStatusApiClient, DocumentStatusApiClient>(x => new DocumentStatusApiClient(x.GetService<IHttpClientFactory>()!, strainLogger, "DocumentStatus"));

            return services;
        }
    }
}

