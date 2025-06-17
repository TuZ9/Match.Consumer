using Microsoft.Extensions.Logging;
using Suitability.Consumer.Application.Interfaces.ApiClientService;
using Suitability.Consumer.Domain.Dto;

namespace Suitability.Consumer.Application.Services.HttpClientBase
{
    public class ProductComplementApiClient : ServiceClientBase<ProductComplementDto, ProductComplementApiClient>, IProductComplementApiClient
    {
        public ProductComplementApiClient(IHttpClientFactory clientFactory, ILogger<ProductComplementApiClient> logger, string clientName) : base(clientFactory, logger, clientName)
        {

        }
    }
}
