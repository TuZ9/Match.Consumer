using Microsoft.Extensions.Logging;
using Suitability.Consumer.Domain.Dto;
using Suitability.Consumer.Domain.Interfaces.ApiClientService;

namespace Suitability.Consumer.Infrastructure.HttpClientBase
{
    public class ProductComplementApiClient : ServiceClientBase<ProductComplementDto, ProductComplementApiClient>, IProductComplementApiClient
    {
        public ProductComplementApiClient(IHttpClientFactory clientFactory, ILogger<ProductComplementApiClient> logger, string clientName) : base(clientFactory, logger, clientName)
        {

        }
    }
}
