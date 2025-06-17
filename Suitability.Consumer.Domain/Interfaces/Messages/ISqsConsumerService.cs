using Suitability.Consumer.Infrastructure.Extensions;
using Amazon.SQS.Model;

namespace Suitability.Consumer.Domain.Interfaces.Messages
{
    public interface ISqsConsumerService
    {
        void PurgeQueue(AwsConfiguration configs);
        void SendMessage(string? qUrl, List<string> messageBody);
        Task<IEnumerable<Message>>GetSqsMessageAsync(AwsConfiguration configs);
        void DeleteMessage(string qUrl, Message message);
        Task PullSqsDlMessage(AwsConfiguration configs, string messageBody);

    }
}
