using Gate.Cloud;
using Gate.Simulators;
using Microsoft.AspNetCore.Mvc;

namespace Gate.Controllers;

[ApiController]
[Route("api/simulator")]
public class SimulatorController : ControllerBase
{
    private readonly List<Guid> _deviceIds = new List<Guid> {
        new Guid("8343a9b1-ad5e-4fcb-9b80-4347229e2f17"),
        new Guid("441cfb3d-9724-4606-a171-638e92545c47"),
        new Guid("3e7e3dcc-c8b7-4c3e-9515-f1206fce6fbf"),
        new Guid("c861e9f7-8aa8-4094-bab4-a505513bcd9b"),
        new Guid("7043044c-b4e5-4cf4-a593-ad6c23813485"),
        new Guid("09712c61-da66-4430-af42-d5cd61d2a5d1"),
        new Guid("cba4e306-45c6-48a2-bfb2-50b24f9458d1"),
        new Guid("6b625711-03db-4ae5-9b80-1755aee86123"),
        new Guid("6d5ebf1e-cf87-4b13-8a80-65fbf8c67f21"),
        new Guid("80807893-9a1a-42e7-a1d0-f55a0acc060f")};

    private readonly CloudPublisher _publisher;
    private readonly DeviceSimulator _deviceSimulator;

    public SimulatorController(CloudPublisher publisher, DeviceSimulator deviceSimulator)
    {
        _publisher = publisher;
        _deviceSimulator = deviceSimulator;
    }

    [HttpPost("send-location")]
    public async Task<IActionResult> SimulateLocation()
    {
        var tasks = new List<Task>();

        foreach(var deviceId in _deviceIds)
        {
            string message = _deviceSimulator.CreateDevicesLocationMessage(deviceId);
            tasks.Add(_publisher.PublishAsync(message));
        }

        await Task.WhenAll(tasks);

        return Ok();
    }
}