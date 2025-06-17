using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.data;
using TaskManagementAPI.dtos;
using TaskManagementAPI.models;

namespace TaskManagementAPI.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskDetailsDto>> GetAllAsync(string? state = null, DateTime? dueDate = null)
    {
        var query = _context.Tasks
            .Include(t => t.State)
            .AsQueryable();

        if (!string.IsNullOrEmpty(state))
        {
            query = query.Where(t => t.State.Name == state);
        }

        if (dueDate.HasValue)
        {
            query = query.Where(t => t.DueDate == dueDate.Value);
        }

        return await query
            .Select(t => new TaskDetailsDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                StateName = t.State.Name,
                stateId = t.StateId,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            })
            .ToListAsync();
    }

    public async Task<TaskDetailsDto?> GetByIdAsync(int id)
    {
        return await _context.Tasks
            .Include(t => t.State)
            .Where(t => t.Id == id)
            .Select(t => new TaskDetailsDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                StateName = t.State.Name,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<TaskDetailsDto> CreateAsync(CreateTaskDto dto)
    {
        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            DueDate = dto.DueDate,
            StateId = dto.StateId
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        // Cargar el estado asociado para incluirlo en el DTO
        await _context.Entry(task).Reference(t => t.State).LoadAsync();

        return new TaskDetailsDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            StateName = task.State.Name,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateTaskDto dto)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return false;

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.DueDate = dto.DueDate;
        task.StateId = dto.StateId;
        task.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}
