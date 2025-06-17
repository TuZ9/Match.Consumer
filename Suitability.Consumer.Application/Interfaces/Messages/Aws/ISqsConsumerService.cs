using Suitability.Consumer.Infrastructure.Extensions;
using Amazon.SQS.Model;

namespace Suitability.Consumer.Application.Interfaces.Messages.Aws
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
