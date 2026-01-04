using System.Text.Json;
using Location.Cloud;

namespace Location.DeviceService;

public class LocationService : BackgroundService
{
    private readonly CloudConsumer _cloudConsumer;
    private readonly LocationPersistence _locationPersistence;

    public LocationService(CloudConsumer cloudConsumer, LocationPersistence locationPersistence)
    {
        _cloudConsumer = cloudConsumer;
        _locationPersistence = locationPersistence;

        _cloudConsumer.MessageReceived += ProcessMessage;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _cloudConsumer.StartAsync();
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _cloudConsumer.StopAsync();
        _cloudConsumer.MessageReceived -= ProcessMessage;

        await base.StopAsync(cancellationToken);
    }

    private void ProcessMessage(string message)
    {
        DeviceLocation? receivedDeviceLocation = null;

        try
        {
            receivedDeviceLocation = JsonSerializer.Deserialize<DeviceLocation>(message);            
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Exception during deserialization message: {message}. Exception: {ex.Message}");
        }

        if(receivedDeviceLocation == null)
        {
            return;
        }

        _locationPersistence.UpdateLocation(receivedDeviceLocation);
    }
}