namespace Gate.Cloud.DependencyInjection;

public static class CloudExtensions
{
    public static void AddCloud(this IServiceCollection services)
    {
        services.AddSingleton<CloudConfiguration>();
        services.AddSingleton<CloudPublisherClient>();
        services.AddSingleton<CloudPublisher>();
    }
}