using Location.Database.Entities;
using Location.DeviceService;

namespace Location.Database.Repositories;

public class DeviceLocationRepository
{
    private readonly DbContext _dbContext;
    public DeviceLocationRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveLocationAsync(DeviceLocation location)
    {
        var locationEntity = new DeviceLocationEntity
        {
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            TimestampUtc = location.TimestampUtc
        };

        await _dbContext.Db
            .Collection("devices")
            .Document(location.DeviceId.ToString())
            .Collection("locations")
            .AddAsync(locationEntity);
    } 

    public async Task<List<DeviceLocationEntity>> GetHistoryAsync(Guid deviceId)
    {
        var query = _dbContext.Db
            .Collection("devices")
            .Document(deviceId.ToString())
            .Collection("locations")
            .OrderByDescending("TimestampUtc")
            .Limit(100);

        var snapshot = await query.GetSnapshotAsync();

        return snapshot.Documents
            .Select(d => d.ConvertTo<DeviceLocationEntity>())
            .ToList();
    }
}