namespace Location.Database;

public class DatabaseConfiguration
{
    public string ProjectId { get; init; }

    public DatabaseConfiguration(IConfiguration configuration)
    {
        ProjectId = configuration["GCP_PROJECT_ID"] ?? throw new InvalidOperationException("GCP_PROJECT_ID is not set");
    }
}