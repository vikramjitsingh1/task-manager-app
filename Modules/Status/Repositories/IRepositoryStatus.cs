using System.Data;
using Microsoft.EntityFrameworkCore;
using TmModule.Infrastructure;
using TmModule.Modules.Status.Models;

namespace TmModule.Modules.Status.Repositories
{
    public interface IRepositoryStatus
    {
        Task SetStatus(StatusTable status);
        Task UpdateStatusAsync(int userId, int taskId, TypeofStatus status);
        Task<List<StatusTable>> GetByUserIdAsync(int userId);
        Task<List<StatusTable>> GetAllAsync();
        Task<StatusTable?> GetAsync(int userId, int taskId);

        Task SaveChangesAsync();
    }

    public class RepositoryStatus(AppDbContext context) : IRepositoryStatus
    {
        private readonly AppDbContext _context = context;
        public async Task SetStatus(StatusTable status)
        {
            if (!Enum.IsDefined(status.CurrentStatus))
                throw new ArgumentException("Invalid status value. Allowed values are: Completed(1) and Pending(0).");

            await _context.Status.AddAsync(status);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(int userId, int taskId, TypeofStatus status)
        {
            var existing = await _context.Status.FindAsync(userId, taskId);

            if (existing == null)
                throw new Exception("Record does not exist");

            if (!Enum.IsDefined(status))
                throw new ArgumentException("Invalid status value. Allowed values are: Completed(1) and Pending(0).");

            existing.CurrentStatus = status;

            await _context.SaveChangesAsync();
        }
        
        public async Task<List<StatusTable>> GetByUserIdAsync(int userId)
        {
            return await _context.Status.Where(s => s.UserId == userId).ToListAsync();
        }
        public async Task<List<StatusTable>> GetAllAsync()
        {
            return await _context.Status.ToListAsync();
        }
        public async Task<StatusTable?> GetAsync(int userId, int taskId)
        {
            return await _context.Status.FindAsync(userId, taskId);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
