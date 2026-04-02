using TmModule.Infrastructure;
using TmModule.Modules.Tasks.Models;
using TmModule.Modules.Tasks.Repositories;
using Microsoft.EntityFrameworkCore;
using TmModule.Application.CoreController;

namespace TmModule.Modules.Tasks.Services
{
    public class TaskService(IRepositoryTask repository)
    {
        // Dependency injection of the repository
        private readonly IRepositoryTask _repository = repository;
 

    //method to get all tasks
    public Task<List<TaskTable>> GetAllTasksAsync()
        {
            return _repository.GetAllTasksAsync();
        }

        //method to get a task by ID
        public Task<TaskTable?> GetTaskByIdAsync(int taskId)
        {
            return _repository.GetTaskbyIdAsync(taskId);
        }

        // Method to add a new task
        public async Task AddTaskAsync(TaskTable task)
        {
            ArgumentNullException.ThrowIfNull(task);

            if (string.IsNullOrWhiteSpace(task.Description))
            {
                throw new ArgumentException("Task description is required", nameof(task));
            }

            await _repository.AddAsync(task);
        }

        // Method to save changes (if needed)
        public Task SaveChangesAsync()
        {
            return _repository.SaveChangesAsync();
        }

    } 
}