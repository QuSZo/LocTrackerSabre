namespace Gate.Cloud;

public class CloudConfiguration
{
    public string ProjectId { get; init; }
    public string TopicId { get; init; }

    public CloudConfiguration(IConfiguration configuration)
    {
        ProjectId = configuration["GCP_PROJECT_ID"] ?? throw new InvalidOperationException("GCP_PROJECT_ID is not set");
        TopicId = configuration["PUBSUB_TOPIC_ID"] ?? throw new InvalidOperationException("PUBSUB_TOPIC_ID is not set");
    }
}