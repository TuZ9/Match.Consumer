using Microsoft.Extensions.Logging;
using Suitability.Consumer.Application.Interfaces.ApiClientService;
using Suitability.Consumer.Domain.Dto;

namespace Suitability.Consumer.Infrastructure.HttpClientBase
{
    public class ProductApiClient : ServiceClientBase<ProductDto, ProductApiClient>, IProductApiClient
    {
        public ProductApiClient(IHttpClientFactory clientFactory, ILogger<ProductApiClient> logger, string clientName) : base(clientFactory, logger, clientName)
        {

        }
    }
}
