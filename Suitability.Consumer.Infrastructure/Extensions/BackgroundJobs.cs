using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suitability.Consumer.Infrastructure.Extensions
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
    }
}
