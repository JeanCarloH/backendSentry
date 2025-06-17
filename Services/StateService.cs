using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.data;
using TaskManagementAPI.dtos;
using TaskManagementAPI.models;

namespace TaskManagementAPI.Services;

public class StateService : IStateService
{
    private readonly AppDbContext _context;

    public StateService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StateDto>> GetAllAsync()
    {
        return await _context.States
            .Select(s => new StateDto
            {
                Id = s.Id,
                Name = s.Name,
                createdAt = s.CreatedAt,
                updatedAt = s.UpdatedAt
            })
            .ToListAsync();
    }

    public async Task<StateDto?> GetByIdAsync(int id)
    {
        return await _context.States
            .Select(s => new StateDto
            {
                Id = s.Id,
                Name = s.Name
            })
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<(bool Success, string Message, StateDto? Data)> CreateAsync(StateDto dto)
    {
        var exists = await _context.States.AnyAsync(s => s.Name == dto.Name);
        if (exists)
            return (false, "El estado ya existe", null);

        var state = new State
        {
            Name = dto.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.States.Add(state);
        await _context.SaveChangesAsync();

        var createdState = await _context.States
     .Where(s => s.Id == state.Id)
     .Select(s => new StateDto
     {
         Id = s.Id,
         Name = s.Name,
         createdAt = s.CreatedAt,
         updatedAt = s.UpdatedAt
     })
     .FirstOrDefaultAsync();

        if (createdState == null)
            return (false, "Error al crear el estado", null);

        return (true, "Estado creado correctamente", createdState);

    }

    public async Task<bool> UpdateAsync(int id, StateDto dto)
    {
        var state = await _context.States.FindAsync(id);
        if (state == null)
            return false;

        state.Name = dto.Name;
        state.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<(bool Success, string Message)> DeleteAsync(int id)
    {
        var state = await _context.States
            .Include(s => s.Tasks)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (state == null)
            return (false, "Estado no encontrado");

        if (state.Tasks.Any())
            return (false, "No se puede eliminar: tiene tareas asociadas");

        _context.States.Remove(state);
        await _context.SaveChangesAsync();
        return (true, "Estado eliminado correctamente");
    }
}
