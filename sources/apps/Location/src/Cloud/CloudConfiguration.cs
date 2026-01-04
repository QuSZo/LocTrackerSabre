namespace Location.Cloud;

public class CloudConfiguration
{
    public string ProjectId { get; init; }
    public string SubscriptionId { get; init; }

    public CloudConfiguration(IConfiguration configuration)
    {
        ProjectId = configuration["GCP_PROJECT_ID"] ?? throw new InvalidOperationException("GCP_PROJECT_ID is not set");
        SubscriptionId = configuration["PUBSUB_SUBSCRIPTION_ID"] ?? throw new InvalidOperationException("PUBSUB_SUBSCRIPTION_ID is not set");
    }
}