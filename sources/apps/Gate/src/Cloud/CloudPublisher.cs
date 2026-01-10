using Google.Cloud.PubSub.V1;
using Google.Protobuf;

namespace Gate.Cloud;

public class CloudPublisher
{
    private readonly CloudPublisherClient _cloudPublisherClient;
    private readonly ILogger<CloudPublisher> _logger;
    
    public CloudPublisher(CloudPublisherClient cloudPublisherClient, ILogger<CloudPublisher> logger)
    {
        _cloudPublisherClient = cloudPublisherClient;
        _logger = logger;
    }

    public async Task PublishAsync(string message)
    {
        var pubSubMessage = new PubsubMessage
        {
            Data = ByteString.CopyFromUtf8(message)
        };

        await _cloudPublisherClient.PublishAsync(pubSubMessage);

        _logger.LogInformation($"Published message: {message}");
    }
}