using Suitability.Consumer.Domain.Interfaces.Services;
using Suitability.Consumer.Domain.Interfaces.ApiClientService;
using Suitability.Consumer.Domain.Dto;

namespace Suitability.Consumer.Application.Services
{
    public class GatewayService : IGatewayService
    {
        private readonly ILogger<GatewayService> _logger;
        private readonly IAccountApiClient _accountClient;
        private readonly IDocumentStatusApiClient _documentStatusClient;

        private readonly IProductApiClient _steamAppClient;
        private readonly IProductComplementApiClient _steamGamesClient;


        public GatewayService(ILogger<GatewayService> logger, IAccountApiClient accountClient, IDocumentStatusApiClient documentStatusClient, IProductApiClient steamAppClient, IProductComplementApiClient steamGamesClient) 
        {
            _logger = logger;
            _accountClient = accountClient;
            _documentStatusClient = documentStatusClient;
            _steamAppClient = steamAppClient;
            _steamGamesClient = steamGamesClient;
        }

        public async Task ConsumeProducts()
        {
            try
            {
                var list = new List<ProductComplementDto>();
                var games = await _steamAppClient.GetAsync($"/ISteamApps/GetAppList/v2/");
                foreach (var g in games.applist.apps)
                {
                    var game = await _steamGamesClient.GetDicAsync($"/api/appdetails?appids={g.appid}");
                    if (game != null)
                    {
                        _logger.LogInformation($"{g.appid}");
                        var a = game.FirstOrDefault();
                        if (a.Value.success == true)
                        {
                            list.Add(a.Value);
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
