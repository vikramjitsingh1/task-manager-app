using System;
using System.Threading.Tasks;
using TmModule.Modules.Status.Repositories;
using TmModule.Modules.Tasks.Repositories;
using TmModule.Modules.Users.Repositories;
using TmModule.Infrastructure.DTOs;
using TmModule.Modules.Users.Models;
using TmModule.Modules.Tasks.Models;
using TmModule.Modules.Status.Models;

namespace TmModule.Application.UseCases
{
    public class CreateUserTask
    {
        private readonly IRepositoryStatus _repositoryStatus;
        private readonly IRepositoryUsers _repositoryUsers;
        private readonly IRepositoryTask _repositoryTask;

        public CreateUserTask(
            IRepositoryStatus repositoryStatus,
            IRepositoryUsers repositoryUsers,
            IRepositoryTask repositoryTask)
        {
            _repositoryStatus = repositoryStatus ?? throw new ArgumentNullException(nameof(repositoryStatus));
            _repositoryUsers = repositoryUsers ?? throw new ArgumentNullException(nameof(repositoryUsers));
            _repositoryTask = repositoryTask ?? throw new ArgumentNullException(nameof(repositoryTask));
        }

        public async Task CreateUserTaskAsync(SimplifiedRequest simplifiedRequest)
        {
            ArgumentNullException.ThrowIfNull(simplifiedRequest);

            // Create user
            var user = new UsersTable
            {
                Username = simplifiedRequest.UserName
            };
            await _repositoryUsers.AddAsync(user);

            // Create task
            var task = new TaskTable
            {
                Description = simplifiedRequest.TaskDescription
            };
            await _repositoryTask.AddAsync(task);

            // Set status directly from request (no casting needed)
            var status = new StatusTable
            {
                UserId = user.UserId,
                TaskId = task.TaskId,
                CurrentStatus = simplifiedRequest.Status
            };

            await _repositoryStatus.SetStatus(status);
        }
    }
}
