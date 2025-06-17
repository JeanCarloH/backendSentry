using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.dtos;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatesController : ControllerBase
{
    private readonly IStateService _stateService;

    public StatesController(IStateService stateService)
    {
        _stateService = stateService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StateDto>>> GetAll()
    {
        var result = await _stateService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StateDto>> GetById(int id)
    {
        var result = await _stateService.GetByIdAsync(id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(StateDto dto)
    {
        var (success, message, createdState) = await _stateService.CreateAsync(dto);
        if (!success)
            return Conflict(message);

        return Ok(new { message, data = createdState });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, StateDto dto)
    {
        var success = await _stateService.UpdateAsync(id, dto);
        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var (success, message) = await _stateService.DeleteAsync(id);
        if (!success)
            return BadRequest(message);

        return Ok(new { message });
    }
}
