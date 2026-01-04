using Google.Cloud.PubSub.V1;

namespace Gate.Cloud;

public class CloudPublisherClient
{
    private readonly PublisherClient _publisherClient;
    
    public CloudPublisherClient(CloudConfiguration configuration)
    {
        var topicName = TopicName.FromProjectTopic(
            configuration.ProjectId,
            configuration.TopicId
        );

        _publisherClient = PublisherClient.Create(topicName);
    }

    public async Task<string> PublishAsync(PubsubMessage message)
    {
        return await _publisherClient.PublishAsync(message);
    }
}