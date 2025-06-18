using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System.Net;
using Suitability.Consumer.Domain.Interfaces.Services;
using Suitability.Consumer.Infrastructure.Static;

namespace Suitability.Consumer.Application.Services
{
    public class AmazonBucketService : IAmazonBucketService
    {
        private readonly DeleteObjectRequest _deleteObjectRequest;
        private readonly GetObjectRequest _getObjectRequest;
        private readonly IAmazonS3 _s3;
        private readonly ILogger<AmazonBucketService> _logger;
        public AmazonBucketService(ILogger<AmazonBucketService> logger) 
        {
            _logger = logger;
            _getObjectRequest = new GetObjectRequest();
            _deleteObjectRequest = new DeleteObjectRequest();
            _s3 = new AmazonS3Client(RegionEndpoint.GetBySystemName(RunTimeConfig.AwsRegion));
        }
        public async Task<string> GetS3LessObjectAsync(string message)
        {
            _getObjectRequest.BucketName = RunTimeConfig.S3Bucket;
            _getObjectRequest.Key = $"position/{message}";
            var response = await _s3.GetObjectAsync(_getObjectRequest);
            if(response.HttpStatusCode != HttpStatusCode.OK) return null;
            var reader = new StreamReader(response.ResponseStream);
            var content = await reader.ReadToEndAsync();
            await DeleteS3Object(message);
            return string.IsNullOrWhiteSpace(content) ? null : content;
        }

        private async Task DeleteS3Object(string key)
        {
            _logger.LogInformation($"Deleting s3 Object {key}");
            _deleteObjectRequest.BucketName = RunTimeConfig.S3Bucket;
            _deleteObjectRequest.Key = $"position/{key}";
            await _s3.DeleteObjectAsync(_deleteObjectRequest);  
        }

        public async Task PurgeBucket(string bucket, string? folder)
        {

            var request = new ListObjectsRequest
            {
                BucketName = bucket
            };
            var response = await _s3.ListObjectsAsync(request);
            var keys = new List<KeyVersion>();
            foreach (var item in response.S3Objects)
            {
                if (item.Key != folder + "/")
                {
                    keys.Add(new KeyVersion { Key = item.Key });
                    _logger.LogInformation($"Add Key for remove: {item.Key}");
                }                
            }
            var multiObjectDeleteRequest = new DeleteObjectRequest()
            {
                BucketName = bucket
            };

            await _s3.DeleteObjectAsync(multiObjectDeleteRequest);
        }
    }
}
