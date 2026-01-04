namespace Location.DeviceService;

public record DeviceLocation
{
    public Guid DeviceId { get; set; } 
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime TimestampUtc { get; set; }

    public DeviceLocation(Guid deviceId, double latitude, double longitude, DateTime timestampUtc)
    {
        DeviceId = deviceId;
        Latitude = latitude;
        Longitude = longitude;
        TimestampUtc = timestampUtc;
    }
}