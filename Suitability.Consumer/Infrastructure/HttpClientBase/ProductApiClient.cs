using Microsoft.Extensions.Logging;
using Suitability.Consumer.Domain.Dto;
using Suitability.Consumer.Domain.Interfaces.ApiClientService;

namespace Suitability.Consumer.Infrastructure.HttpClientBase
{
    public class ProductApiClient : ServiceClientBase<ProductDto, ProductApiClient>, IProductApiClient
    {
        public ProductApiClient(IHttpClientFactory clientFactory, ILogger<ProductApiClient> logger, string clientName) : base(clientFactory, logger, clientName)
        {

        }
    }
}
