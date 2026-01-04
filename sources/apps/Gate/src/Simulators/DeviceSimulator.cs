using System.Text.Json;

namespace Gate.Simulators;

public class DeviceSimulator
{
    private const double MaxLatitude = 50.08;
    private const double MaxLongitude = 19.93;
    private const double MinLatitude = 50.06;
    private const double MinLongitude = 19.88;
    private static readonly Random _random = new();

    public DeviceSimulator()
    {
    }

    public string CreateDevicesLocationMessage(Guid deviceId)
    {
        DeviceLocationDto deviceLocationDto = GenerateDeviceLocation(deviceId);
        
        return JsonSerializer.Serialize(deviceLocationDto);
    }

    private DeviceLocationDto GenerateDeviceLocation(Guid deviceId)
    {
        double latitude = GenerateRandomLocation(MinLatitude, MaxLatitude);
        double longitude = GenerateRandomLocation(MinLongitude, MaxLongitude);

        return new DeviceLocationDto(
            DeviceId: deviceId,
            Latitude: latitude,
            Longitude: longitude,
            TimestampUtc: DateTime.UtcNow
        );
    }

    private static double GenerateRandomLocation(double min, double max)
    {
        return min + (_random.NextDouble() * (max - min));
    }
}