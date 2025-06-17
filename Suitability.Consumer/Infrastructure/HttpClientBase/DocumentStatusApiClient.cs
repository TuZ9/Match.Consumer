using Microsoft.Extensions.Logging;
using Suitability.Consumer.Domain.Entities;
using Suitability.Consumer.Domain.Interfaces.ApiClientService;

namespace Suitability.Consumer.Infrastructure.HttpClientBase
{
    public class DocumentStatusApiClient :ServiceClientBase<DocumentStatus, DocumentStatusApiClient>, IDocumentStatusApiClient
    {
        public DocumentStatusApiClient(IHttpClientFactory clientFactory, ILogger<DocumentStatusApiClient> logger, string clientName) : base(clientFactory, logger, clientName)
        {

        }
    }
}
