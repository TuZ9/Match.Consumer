using Microsoft.Extensions.Logging;
using Suitability.Consumer.Application.Interfaces.ApiClientService;
using Suitability.Consumer.Domain.Entities;

namespace Suitability.Consumer.Infrastructure.HttpClientBase
{
    public class DocumentStatusApiClient : ServiceClientBase<DocumentStatus, DocumentStatusApiClient>, IDocumentStatusApiClient
    {
        public DocumentStatusApiClient(IHttpClientFactory clientFactory, ILogger<DocumentStatusApiClient> logger, string clientName) : base(clientFactory, logger, clientName)
        {

        }
    }
}
