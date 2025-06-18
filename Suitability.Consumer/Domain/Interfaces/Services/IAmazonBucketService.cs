namespace Suitability.Consumer.Domain.Interfaces.Services
{
    public interface IAmazonBucketService
    {
        Task<string> GetS3LessObjectAsync(string message);
        Task PurgeBucket(string bucket, string? folder);
    }
}
