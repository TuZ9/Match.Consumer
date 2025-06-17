using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Amazon.SQS.Model;
using Suitability.Consumer.Domain.Interfaces.Messages;

namespace Suitability.Consumer.Application.Services.BackgroundJobs
{
    public class BackgroundJobs : BackgroundService
    {
        private readonly ILogger<BackgroundJobs> _logger;
        private readonly IServiceProvider _service;
        private SemaphoreSlim _semaphore;
        public BackgroundJobs(ILogger<BackgroundJobs> logger, IServiceProvider service)
        {
            _service = service;
            _logger = logger;
            _semaphore = new(1,1);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var job = Task.Run(() => Start(stoppingToken), stoppingToken);
            _logger.LogInformation("Start Background Jobs");
            //await Task.WhenAll(job);
            await job;
        }

        private async Task Start(CancellationToken tokenStop)
        {
            await using var scoped = _service.CreateAsyncScope();
            var scopedService = scoped.ServiceProvider;

            while (!tokenStop.IsCancellationRequested)
            {

            }
        }

        private async Task<IEnumerable<Message>> GetMessages(Domain.Helper.AwsConfiguration configs)
        {
            try
            {
                await using var scoped = _service.CreateAsyncScope();
                var scopedService = scoped.ServiceProvider.GetRequiredService<ISqsConsumerService>;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
            }
            return new List<Message>();
        }
    }
}
