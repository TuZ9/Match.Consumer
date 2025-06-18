using Amazon.SQS.Model;
using System.Text.Json;
using Suitability.Consumer.Domain.Entities.Messages;
using Suitability.Consumer.Domain.Helper;
using Suitability.Consumer.Domain.Interfaces.Messages;

namespace Suitability.Consumer.Application.Services.BackgroundJobs
{
    public class BackgroundJobs : BackgroundService
    {
        private readonly ILogger<BackgroundJobs> _logger;
        private readonly IServiceProvider _service;
        private int _processMessagesCount;
        private SemaphoreSlim _semaphore;
        public BackgroundJobs(ILogger<BackgroundJobs> logger, IServiceProvider service)
        {
            _service = service;
            _logger = logger;
            _semaphore = new(1,1);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queueConfig = new AwsConfiguration
            {
                SQSUri = Environment.GetEnvironmentVariable("SQS_SUITABILITY"),
                AWSRegion = Environment.GetEnvironmentVariable("AWS_DEFAULT_REGION"),
                SQSDLUri = Environment.GetEnvironmentVariable("SQS_SUITABILITY_DL"),
            };
            _logger.LogInformation("Start Background Jobs");
            await Task.Run(() => Start(queueConfig, stoppingToken), stoppingToken); 
        }

        private async Task Start(AwsConfiguration config, CancellationToken tokenStop)
        {
            using var scopeds = _service.CreateAsyncScope();
            var scopedService = scopeds.ServiceProvider.GetRequiredService<IReadMessageService>();

            while (!tokenStop.IsCancellationRequested)
            {
                List<Task> work = new();

                var messages = await GetMessages(config);

                var listMessage = messages as Message[] ?? messages.ToArray();

                if (listMessage.Any() && !listMessage.Any(x => x.Body.Contains(".json")))
                {
                    foreach (var msg in listMessage)
                    {
                        try
                        {
                            var message = MessageLess.TryBuildLessMessage(JsonSerializer.Deserialize<MessageLess>(msg.Body));

                            await _semaphore.WaitAsync(tokenStop);
                            _processMessagesCount++;

                            work.Add(Task.Run(async () =>
                            {
                                await scopedService.InitialProcessingSqsAsync(message, msg.MessageId).ContinueWith(res =>
                                {
                                    _semaphore.Release();
                                }, tokenStop);
                            }));
                        }
                        catch (Exception e)
                        {
                            _logger.LogInformation($"Error: {e}");
                            PullSqsDlMessage(config, msg);
                            ClearReferences(messages);
                        }                       

                    }
                    ClearReferences(listMessage);
                    await Task.WhenAll(work);
                    _logger.LogInformation($"{_processMessagesCount} messages were processed");
                }
                else
                {
                    if (!listMessage.Any() || !listMessage.Any(x => x.Body.Contains(".json"))) continue;
                    {
                        foreach(var msg in listMessage)
                        {
                            try
                            {
                                await scopedService.InitialProcessingS3Async(msg.Body);
                            }
                            catch (Exception e)
                            {
                                _logger.LogInformation($"Error: {e}");
                                PullSqsDlMessage(config, msg);
                                ClearReferences(listMessage);
                            }
                        }
                    }
                }
            }
        }

        private void PullSqsDlMessage(AwsConfiguration configs, Message messages)
        {                        
            try
            {
                using var scopeds = _service.CreateAsyncScope();
                var scopedService = scopeds.ServiceProvider.GetRequiredService<ISqsConsumerService>();
                scopedService.PullSqsDlMessage(configs, messages.Body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private static void ClearReferences(IEnumerable<Message> messages)
        {
            messages.ToList().Clear();
        }

        private async Task<IEnumerable<Message>> GetMessages(AwsConfiguration configs)
        {
            try
            {
                using var scoped = _service.CreateAsyncScope();
                var scopedService = scoped.ServiceProvider.GetRequiredService<ISqsConsumerService>();
                return await scopedService.GetSqsMessageAsync(configs);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
            }
            return new List<Message>();
        }
    }
}
