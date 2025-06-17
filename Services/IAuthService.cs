using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public interface IAuthService
    {
        Task<User?> ValidateUserAsync(string email, string password);
    }
}
