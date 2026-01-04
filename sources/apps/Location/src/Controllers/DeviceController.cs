using Location.DeviceService;
using Microsoft.AspNetCore.Mvc;

namespace Location.Controllers;

[ApiController]
[Route("api/device")]
public class DeviceController : ControllerBase
{
    private readonly LocationPersistence _locationPersistence;

    public DeviceController(LocationPersistence locationPersistence)
    {
        _locationPersistence = locationPersistence;
    }

    [HttpGet("locations")]
    public ActionResult<List<DeviceLocation>> GetLocations()
    {
        List<DeviceLocation> deviceLocations = _locationPersistence.GetDeviceLocations;

        return Ok(deviceLocations);
    }
}