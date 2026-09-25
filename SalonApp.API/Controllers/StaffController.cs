using Microsoft.AspNetCore.Mvc;
using SalonApp.Modules.Staff.Application;
using SalonApp.Modules.Staff.Domain;

namespace SalonApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaffController : ControllerBase
{
    private readonly StaffService _staffService;

    public StaffController(StaffService staffService)
    {
        _staffService = staffService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var staff = await _staffService.GetAllStaffMembersAsync();
        return Ok(staff);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var member = await _staffService.GetStaffMemberByIdAsync(id);
        if (member == null) return NotFound();
        return Ok(member);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] StaffMember staffMember)
    {
        try
        {
            await _staffService.AddStaffMemberAsync(staffMember);
            return Ok(staffMember);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] StaffMember staffMember)
    {
        try
        {
            staffMember.Id = id;
            await _staffService.UpdateStaffMemberAsync(staffMember);
            return Ok(staffMember);
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
            await _staffService.DeleteStaffMemberAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}