using TmModule.Infrastructure;
using TmModule.Modules.Status.Models;
using TmModule.Modules.Status.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TmModule.Modules.Status.Services
{
    public class StatusUpdateService(IRepositoryStatus repo)
    {
        // Dependency injection of the repository
        private readonly IRepositoryStatus _repo = repo;

        // Method to assign status to a task for a user
        public async Task AssignTask(int userId, int taskId)
        {
            var status = new StatusTable
            {
                UserId = userId,
                TaskId = taskId,
                CurrentStatus = TypeofStatus.Pending
            };

            await _repo.SetStatus(status);
        }

        //Method to update the status of a task for a user
        public async Task UpdateStatus(int userId, int taskId, TypeofStatus newStatus)
        {
            if (!Enum.IsDefined(newStatus))
                throw new ArgumentException("Invalid status");

            await _repo.UpdateStatusAsync(userId, taskId, newStatus);
        }

        // Method to get the status of a task for a user
        public Task<StatusTable?> GetStatusAsync(int userId, int taskId)
        {
            return _repo.GetAsync(userId, taskId);
        }

        /* Method to get all statuses for a user
        public Task<List<StatusTable>> GetStatusesByUserIdAsync(int userId)
        {
            return _repo.GetByUserIdAsync(userId);
        }

        // Method to get all statuses for a task
        public Task<List<StatusTable>> GetStatusesByTaskIdAsync(int taskId)
        {
            return _repo.GetByTaskIdAsync(taskId);
        } */


        // Method to save changes to the database
        public Task SaveChangesAsync()
        {
            return _repo.SaveChangesAsync();
        }
    }
}
