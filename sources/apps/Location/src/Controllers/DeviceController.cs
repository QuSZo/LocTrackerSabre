using System.Threading.Tasks;
using Location.Database.Entities;
using Location.Database.Repositories;
using Location.DeviceService;
using Microsoft.AspNetCore.Mvc;

namespace Location.Controllers;

[ApiController]
[Route("api/device")]
public class DeviceController : ControllerBase
{
    private readonly LocationPersistence _locationPersistence;
    private readonly DeviceLocationRepository _repository;

    public DeviceController(LocationPersistence locationPersistence, DeviceLocationRepository repository)
    {
        _locationPersistence = locationPersistence;
        _repository = repository;
    }

    [HttpGet("locations")]
    public ActionResult<List<DeviceLocation>> GetLocations()
    {
        List<DeviceLocation> deviceLocations = _locationPersistence.GetDeviceLocations;

        return Ok(deviceLocations);
    }
    
    [HttpGet("{id:guid}/locations/history")]
    public async Task<ActionResult<IReadOnlyList<DeviceLocationEntity>>> GetHistoryLocationByDeviceId([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("DeviceId cannot be empty.");

        var deviceHistoryLocations = await _repository.GetHistoryAsync(id);

        if (deviceHistoryLocations == null || deviceHistoryLocations.Count == 0)
            return NotFound();

        return Ok(deviceHistoryLocations);
    }
}