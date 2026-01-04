namespace Gate.Simulators;

public record DeviceLocationDto(Guid DeviceId, double Latitude, double Longitude, DateTime TimestampUtc);