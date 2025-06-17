using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.data;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> ValidateUserAsync(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u =>
                u.Email == email && u.Password == password); // ⚠️ idealmente usar hash
        }
    }
}
