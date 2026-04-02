using Microsoft.EntityFrameworkCore;
using TmModule.Infrastructure;
using TmModule.Modules.Users.Models;

namespace TmModule.Modules.Users.Repositories
{
    public interface IRepositoryUsers
    {
        Task<List<UsersTable>> GetAllUsersAsync();
        Task<UsersTable?> GetUserByIdAsync(int userId);
        Task AddAsync(UsersTable user);
        Task SaveChangesAsync();
    }

    public class RepositoryUsers(AppDbContext context) : IRepositoryUsers
    {
        private readonly AppDbContext _context = context;

        public async Task<List<UsersTable>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<UsersTable?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }
        public async Task AddAsync(UsersTable user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
