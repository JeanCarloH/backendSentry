using TaskManagementAPI.dtos;

namespace TaskManagementAPI.Services;

public interface IStateService
{
    Task<IEnumerable<StateDto>> GetAllAsync();
    Task<StateDto?> GetByIdAsync(int id);
    Task<(bool Success, string Message, StateDto? Data)> CreateAsync(StateDto dto);
    Task<bool> UpdateAsync(int id, StateDto dto);
    Task<(bool Success, string Message)> DeleteAsync(int id);
}
