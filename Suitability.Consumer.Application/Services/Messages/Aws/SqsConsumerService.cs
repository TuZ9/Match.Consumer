using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using Suitability.Consumer.Application.Interfaces.Messages.Aws;
using Suitability.Consumer.Infrastructure.Extensions;

namespace Suitability.Consumer.Application.Services.Messages.Aws
{
    public class SqsConsumerService : ISqsConsumerService
    {
        private readonly ILogger<SqsConsumerService> _logger;
        private AmazonSQSClient _amazonSQSClient;
        public SqsConsumerService(ILogger<SqsConsumerService> logger) 
        {
            _logger = logger;
            _amazonSQSClient = new();
        }
        public void DeleteMessage(string qUrl, Message message)
        {
            try
            {
                var messageReceptorHandle = message?.ReceiptHandle;
                _amazonSQSClient.DeleteMessageAsync(qUrl, messageReceptorHandle);
            }
            catch (Exception e)
            {
                _logger.LogError("Error {0}", e.StackTrace);
                _logger.LogError("Error {0}", e.Message);
            }
        }

        public async Task<IEnumerable<Message>> GetSqsMessageAsync(AwsConfiguration configs)
        {
            var list = new List<Message>();
            var _sqsClient = SqsClientConfiguration(configs);
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = configs.SQSDLUri,
                MaxNumberOfMessages = 10,
                VisibilityTimeout = (int)TimeSpan.FromSeconds(200).TotalSeconds,
                WaitTimeSeconds = 2
            };
            try
            {
                var messageList = await _sqsClient.ReceiveMessageAsync(receiveMessageRequest);
                var listMessage = messageList.Messages.DistinctBy(x => x.MessageId).ToList();
                foreach (var message in listMessage)
                {
                    try
                    {
                        list.Add(message);
                        if (configs.SQSUri != null) DeleteMessage(configs.SQSDLUri, message);
                    }
                    catch (Exception e)
                    {
                        if (configs.SQSUri != null) DeleteMessage(configs.SQSDLUri, message);                       
                        throw new Exception(e.Message);
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                _logger.LogError("Error {0}", e.StackTrace);
                throw new Exception(e.Message);
            }            
        }

        public async Task PullSqsDlMessage(AwsConfiguration configs, string messageBody)
        {
            var _sqsClient = SqsClientConfiguration(configs);

            try
            {
                var sendMessageRequest = new SendMessageRequest
                {
                    QueueUrl = configs.SQSDLUri,
                    MessageBody = messageBody,
                    MessageGroupId = Guid.NewGuid().ToString(),
                    MessageDeduplicationId = Guid.NewGuid().ToString()
                };

                await _sqsClient.SendMessageAsync(configs.SQSDLUri, messageBody);
            }
            catch (Exception e)
            {
                _logger.LogError("Error {0}", e.StackTrace);
                throw new Exception(e.Message);
            }
        }

        public void PurgeQueue(AwsConfiguration configs)
        {
            try
            {
                _amazonSQSClient.PurgeQueueAsync(configs.SQSUri);
            }
            catch (Exception e)
            {
                _logger.LogError("Error {0}", e.StackTrace);
                throw new Exception(e.Message);
            }
        }

        private AmazonSQSClient SqsClientConfiguration(AwsConfiguration awsConfiguration)
        {
            var region = RegionEndpoint.GetBySystemName(awsConfiguration.AWSRegion);
            if (_amazonSQSClient != null) return _amazonSQSClient;
            var sqsClient = new AmazonSQSClient(region);
            _amazonSQSClient = sqsClient;

            return _amazonSQSClient;
        }

        public void SendMessage(string? qUrl, List<string> messageBody)
        {
            foreach (var message in messageBody) _amazonSQSClient.SendMessageAsync(qUrl, message).Wait();            
        }
    }
}
