using Google.Cloud.PubSub.V1;

namespace Location.Cloud;

public class CloudConsumer
{
    private readonly SubscriberClient _subscriberClient;
    
    public CloudConsumer(CloudConfiguration configuration)
    {
        var subscriptionName = SubscriptionName.FromProjectSubscription(
            configuration.ProjectId,
            configuration.SubscriptionId);

        _subscriberClient = SubscriberClient.Create(subscriptionName);
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
            return success
                ? SubscriberClient.Reply.Ack
                : SubscriberClient.Reply.Nack;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
            return SubscriberClient.Reply.Nack;
        }
    }

    private bool ProcessMessage(PubsubMessage message)
    {
        string data = message.Data.ToStringUtf8();
        Console.WriteLine($"Odebrano: {data}");

        MessageReceived?.Invoke(data);

        return true;
    }
}