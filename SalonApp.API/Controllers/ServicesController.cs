using Microsoft.AspNetCore.Mvc;
using SalonApp.Modules.Services.Application;
using SalonApp.Modules.Services.Domain;

namespace SalonApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly ServiceManager _serviceManager;

    public ServicesController(ServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var services = await _serviceManager.GetAllServicesAsync();
        return Ok(services);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var service = await _serviceManager.GetServiceByIdAsync(id);
        if (service == null) return NotFound();
        return Ok(service);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Service service)
    {
        try
        {
            await _serviceManager.AddServiceAsync(service);
            return Ok(service);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Service service)
    {
        try
        {
            service.Id = id;
            await _serviceManager.UpdateServiceAsync(service);
            return Ok(service);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _serviceManager.DeleteServiceAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}