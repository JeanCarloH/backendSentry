using TaskManagementAPI.dtos;

namespace TaskManagementAPI.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDetailsDto>> GetAllAsync(string? state = null, DateTime? dueDate = null);
        Task<TaskDetailsDto?> GetByIdAsync(int id);
        Task<TaskDetailsDto> CreateAsync(CreateTaskDto dto);
        Task<bool> UpdateAsync(int id, UpdateTaskDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
