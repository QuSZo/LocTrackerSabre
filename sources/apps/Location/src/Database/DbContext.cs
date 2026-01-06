using Google.Cloud.Firestore;

namespace Location.Database;

public class DbContext
{
    private readonly FirestoreDb _db;

    public DbContext(DatabaseConfiguration configuration)
    {
        string projectId = configuration.ProjectId;

        _db = FirestoreDb.Create(projectId);
    }

    public FirestoreDb Db => _db;
}