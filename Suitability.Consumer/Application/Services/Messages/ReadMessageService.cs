using Suitability.Consumer.Domain.Entities.Messages;
using Suitability.Consumer.Domain.Interfaces.Messages;
using Suitability.Consumer.Domain.Interfaces.Services;
using System.Globalization;
using System.Text.Json;

namespace Suitability.Consumer.Application.Services.Messages
{
    public class ReadMessageService : IReadMessageService
    {
        private readonly IAmazonBucketService _amazonBucketService;
        private readonly ILogger<ReadMessageService> _logger;
        //private readonly 
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        private MessageLess? _lessPayload;
        public ReadMessageService(ILogger<ReadMessageService> logger, IAmazonBucketService amazonBucketService)
        {
            _logger = logger;
            _amazonBucketService = amazonBucketService;
            _lessPayload = new MessageLess();

        }

        public async Task InitialProcessingSqsAsync(MessageLess? message, string? messageId = null)
        {
            try
            {
                var cultureInfo = new CultureInfo("pt-BR");
                await _semaphore.WaitAsync();
                _logger.LogInformation($"Start Process AccountNumber: {_lessPayload?.AccountNumber}");

                if (message == null || _lessPayload?.PositionDate == "") return;          

                var positionDate = DateTime.Parse(_lessPayload?.PositionDate!, cultureInfo).ToString("yyyy-MM-dd");
                var lastDay = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");

                if (DateTime.Parse(positionDate) >= DateTime.Parse(lastDay))
                {
                    //await _
                }
                else
                {
                    _logger.LogInformation($"Account {_lessPayload?.AccountNumber} not processed, due to the date of its position {_lessPayload?.PositionDate}");
                }

            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task InitialProcessingS3Async(string message)
        {
            try
            {
                await _semaphore.WaitAsync();
                var bucketValue = await _amazonBucketService.GetS3LessObjectAsync(message);
                if (bucketValue != null || !string.IsNullOrWhiteSpace(bucketValue))
                {
                    _lessPayload = MessageLess.TryBuildLessMessage(JsonSerializer.Deserialize<MessageLess>(bucketValue));
                    var cultureInfo = new CultureInfo("pt-BR");
                    _logger.LogInformation($"Start Process AccountNumber: {_lessPayload?.AccountNumber}");

                    if (message == null || _lessPayload?.PositionDate == "") return;

                    var positionDate = DateTime.Parse(_lessPayload?.PositionDate!, cultureInfo).ToString("yyyy-MM-dd");
                    var lastDay = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");

                    if (DateTime.Parse(positionDate) >= DateTime.Parse(lastDay))
                    {
                        //await _
                    }
                    else
                    {
                        _logger.LogInformation($"Account {_lessPayload?.AccountNumber} not processed, due to the date of its position {_lessPayload?.PositionDate}");
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                _semaphore.Release();
            }
        }

    }
}