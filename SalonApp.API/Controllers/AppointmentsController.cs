using Microsoft.AspNetCore.Mvc;
using SalonApp.Modules.Appointments.Application;
using SalonApp.Modules.Appointments.Domain;

namespace SalonApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly SchedulingService _schedulingService;

    public AppointmentsController(SchedulingService schedulingService)
    {
        _schedulingService = schedulingService;
    }

    [HttpGet("staff/{staffId}/date/{date}")]
    public async Task<IActionResult> GetByStaffAndDate(int staffId, DateTime date)
    {
        var appointments = await _schedulingService
            .GetAppointmentsByStaffAndDateAsync(staffId, date);
        return Ok(appointments);
    }

    [HttpGet("client/{clientId}")]
    public async Task<IActionResult> GetByClient(int clientId)
    {
        var appointments = await _schedulingService
            .GetAppointmentsByClientAsync(clientId);
        return Ok(appointments);
    }

    [HttpGet("available-slots")]
    public async Task<IActionResult> GetAvailableSlots(
        [FromQuery] int staffId,
        [FromQuery] DateTime date,
        [FromQuery] int durationInMinutes)
    {
        var slots = await _schedulingService.FindAvailableSlotsAsync(
            staffId,
            date,
            durationInMinutes,
            new TimeSpan(9, 0, 0),
            new TimeSpan(17, 0, 0));
        return Ok(slots);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentRequest request)
    {
        try
        {
            var appointment = await _schedulingService.CreateAppointmentAsync(
                request.ClientId,
                request.StaffMemberId,
                request.DateTime,
                request.ServiceId,
                request.DurationInMinutes);
            return Ok(appointment);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> Cancel(int id)
    {
        try
        {
            await _schedulingService.CancelAppointmentAsync(id);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public class CreateAppointmentRequest
{
    public int ClientId { get; set; }
    public int StaffMemberId { get; set; }
    public int ServiceId { get; set; }
    public DateTime DateTime { get; set; }
    public int DurationInMinutes { get; set; }
}