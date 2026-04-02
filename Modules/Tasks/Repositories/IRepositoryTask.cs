using Microsoft.AspNetCore;
using TmModule.Modules.Tasks;
using TmModule.Infrastructure;
using TmModule.Modules.Tasks.Models;
using Microsoft.EntityFrameworkCore;
using TmModule.Application.CoreController;

namespace TmModule.Modules.Tasks.Repositories
{
    public interface IRepositoryTask
    {
        Task<List<TaskTable>> GetAllTasksAsync();
        Task<TaskTable?> GetTaskbyIdAsync(int id);
        Task AddAsync(TaskTable task);
        Task SaveChangesAsync();
    }

    public class RepositoryTask(AppDbContext context) : IRepositoryTask
    {
        private readonly AppDbContext _context = context;
        public async Task<List<TaskTable>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }            
        
        public async Task<TaskTable?> GetTaskbyIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }
        public async Task AddAsync(TaskTable task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
