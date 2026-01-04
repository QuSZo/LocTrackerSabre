using Google.Cloud.PubSub.V1;
using Google.Protobuf;

namespace Gate.Cloud;

public class CloudPublisher
{
    private readonly CloudPublisherClient _cloudPublisherClient;
    
    public CloudPublisher(CloudPublisherClient cloudPublisherClient)
    {
        _cloudPublisherClient = cloudPublisherClient;
    }

    public async Task PublishAsync(string message)
    {
        var pubSubMessage = new PubsubMessage
        {
            Data = ByteString.CopyFromUtf8(message)
        };

        await _cloudPublisherClient.PublishAsync(pubSubMessage);
    }
}