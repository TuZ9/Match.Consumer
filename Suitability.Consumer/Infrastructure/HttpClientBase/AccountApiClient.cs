using Microsoft.Extensions.Logging;
using Suitability.Consumer.Domain.Entities;
using Suitability.Consumer.Domain.Interfaces.ApiClientService;

namespace Suitability.Consumer.Infrastructure.HttpClientBase
{
    public class AccountApiClient : ServiceClientBase<Account, AccountApiClient>, IAccountApiClient
    {
        public AccountApiClient(IHttpClientFactory clientFactory, ILogger<AccountApiClient> logger, string clientName) : base(clientFactory, logger, clientName)
        {

        }
    }
}
