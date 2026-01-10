using Google.Cloud.PubSub.V1;

namespace Location.Cloud;

public class CloudConsumer
{
    private readonly SubscriptionName _subscriptionName; 
    private readonly SubscriberClient _subscriberClient;
    private readonly ILogger<CloudConsumer> _logger;
    
    public CloudConsumer(CloudConfiguration configuration, ILogger<CloudConsumer> logger)
    {
        _logger = logger;

        _subscriptionName = SubscriptionName.FromProjectSubscription(
            configuration.ProjectId,
            configuration.SubscriptionId);

        _subscriberClient = SubscriberClient.Create(_subscriptionName);
    }

    public event Action<string>? MessageReceived;

    public async Task StartAsync()
    {
        await _subscriberClient.StartAsync(HandleMessageAsync);
    }

    public async Task StopAsync()
    {
        await _subscriberClient.StopAsync(CancellationToken.None);
    }

    private async Task<SubscriberClient.Reply> HandleMessageAsync(
        PubsubMessage message,
        CancellationToken token)
    {
        try
        {
            bool success = ProcessMessage(message);
            return success ? SubscriberClient.Reply.Ack : SubscriberClient.Reply.Nack;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error during processing message on subscription name: {_subscriptionName.ToString}. Error: {ex.Message}");
            return SubscriberClient.Reply.Nack;
        }
    }

    private bool ProcessMessage(PubsubMessage message)
    {
        string data = message.Data.ToStringUtf8();
        _logger.LogInformation($"Received message: {data}");

        MessageReceived?.Invoke(data);

        return true;
    }
}