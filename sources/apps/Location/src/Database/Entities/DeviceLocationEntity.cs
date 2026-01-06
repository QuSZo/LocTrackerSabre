using Google.Cloud.Firestore;

namespace Location.Database.Entities;

[FirestoreData]
public class DeviceLocationEntity
{
    [FirestoreProperty]
    public double Latitude { get; set; }

    [FirestoreProperty]
    public double Longitude { get; set; }

    [FirestoreProperty]
    public DateTime TimestampUtc { get; set; }
}