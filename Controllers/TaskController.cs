using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.data;
using TaskManagementAPI.dtos;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly AppDbContext _context;

    public TasksController(ITaskService taskService, AppDbContext context)
    {
        _taskService = taskService;
        _context = context;
    }

    // GET: /api/tasks?state=En%20Progreso&dueDate=2025-06-17
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskDetailsDto>>> GetAll([FromQuery] string? state, [FromQuery] DateTime? dueDate)
    {
        var tasks = await _taskService.GetAllAsync(state, dueDate);
        return Ok(tasks);
    }

    // GET: /api/tasks/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDetailsDto>> GetById(int id)
    {
        var task = await _taskService.GetByIdAsync(id);
        if (task == null)
            return NotFound();
        return Ok(task);
    }

    // POST: /api/tasks
    [HttpPost]
    public async Task<ActionResult<TaskDetailsDto>> Create(CreateTaskDto dto)
    {
        var created = await _taskService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: /api/tasks/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTaskDto dto)
    {
        var result = await _taskService.UpdateAsync(id, dto);
        if (!result) return NotFound();
        return NoContent();
    }

    // DELETE: /api/tasks/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _taskService.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    // GET: /api/tasks/states
    [HttpGet("states")]
    public async Task<ActionResult<IEnumerable<StateDto>>> GetStates()
    {
        var states = await _context.States
            .Select(s => new StateDto { Id = s.Id, Name = s.Name })
            .ToListAsync();

        return Ok(states);
    }
}
