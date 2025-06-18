using Suitability.Consumer.Domain.Entities.Messages;

namespace Suitability.Consumer.Domain.Interfaces.Messages
{
    public interface IReadMessageService
    {
        Task InitialProcessingS3Async(string message);

        Task InitialProcessingSqsAsync(MessageLess? message, string? messageId = null);
    }
}
