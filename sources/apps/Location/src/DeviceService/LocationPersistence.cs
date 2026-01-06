using Location.Database.Repositories;

namespace Location.DeviceService;

public class LocationPersistence
{
    private readonly DeviceLocationRepository _repository;
    private readonly List<DeviceLocation> deviceLocations = new List<DeviceLocation> {
        new DeviceLocation(new Guid("8343a9b1-ad5e-4fcb-9b80-4347229e2f17"), default, default, default),
        new DeviceLocation(new Guid("441cfb3d-9724-4606-a171-638e92545c47"), default, default, default),
        new DeviceLocation(new Guid("3e7e3dcc-c8b7-4c3e-9515-f1206fce6fbf"), default, default, default),
        new DeviceLocation(new Guid("c861e9f7-8aa8-4094-bab4-a505513bcd9b"), default, default, default),
        new DeviceLocation(new Guid("7043044c-b4e5-4cf4-a593-ad6c23813485"), default, default, default),
        new DeviceLocation(new Guid("09712c61-da66-4430-af42-d5cd61d2a5d1"), default, default, default),
        new DeviceLocation(new Guid("cba4e306-45c6-48a2-bfb2-50b24f9458d1"), default, default, default),
        new DeviceLocation(new Guid("6b625711-03db-4ae5-9b80-1755aee86123"), default, default, default),
        new DeviceLocation(new Guid("6d5ebf1e-cf87-4b13-8a80-65fbf8c67f21"), default, default, default),
        new DeviceLocation(new Guid("80807893-9a1a-42e7-a1d0-f55a0acc060f"), default, default, default)};

    private readonly object _lock = new();

    public LocationPersistence(DeviceLocationRepository repository)
    {
        _repository = repository;
    }

    public List<DeviceLocation> GetDeviceLocations => deviceLocations;

    public void UpdateLocation(DeviceLocation receivedDeviceLocation)
    {
        lock (_lock)
        {
            DeviceLocation? deviceLocation = deviceLocations.FirstOrDefault(x => x.DeviceId == receivedDeviceLocation.DeviceId);

            if(deviceLocation != null)
            {
                deviceLocation.Latitude = receivedDeviceLocation.Latitude;
                deviceLocation.Longitude = receivedDeviceLocation.Longitude;
                deviceLocation.TimestampUtc = receivedDeviceLocation.TimestampUtc;

                _repository.SaveLocationAsync(deviceLocation).GetAwaiter().GetResult();
            }
        }
    }
}