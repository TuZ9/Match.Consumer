using Microsoft.Extensions.Logging;
using Suitability.Consumer.Application.Interfaces.ApiClientService;
using Suitability.Consumer.Domain.Entities;

namespace Suitability.Consumer.Application.Services.HttpClientBase
{
    public class AccountApiClient : ServiceClientBase<Account, AccountApiClient>, IAccountApiClient
    {
        public AccountApiClient(IHttpClientFactory clientFactory, ILogger<AccountApiClient> logger, string clientName) : base(clientFactory, logger, clientName)
        {

        }
    }
}
