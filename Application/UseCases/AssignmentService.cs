using System;
using System.Threading.Tasks;
using TmModule.Infrastructure.DTOs;
using TmModule.Modules.Status.Models;
using TmModule.Modules.Status.Repositories;
using TmModule.Modules.Tasks.Repositories;
using TmModule.Modules.Users.Repositories;

namespace TmModule.Application.UseCases
{
        public class AssignTaskToUser(
            IRepositoryStatus repositoryStatus,
            IRepositoryUsers repositoryUsers,
            IRepositoryTask repositoryTask)
        {
            private readonly IRepositoryStatus _repositoryStatus = repositoryStatus ?? throw new ArgumentNullException(nameof(repositoryStatus));
            private readonly IRepositoryUsers _repositoryUsers = repositoryUsers ?? throw new ArgumentNullException(nameof(repositoryUsers));
            private readonly IRepositoryTask _repositoryTask = repositoryTask ?? throw new ArgumentNullException(nameof(repositoryTask));


            public async Task AssignTasksToUsersAsync(AssignTaskToUserRequest request)
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request));

                if (request.TaskId == null || request.TaskId.Length == 0)
                    throw new ArgumentException("At least one task must be specified.", nameof(request.TaskId));

                if (request.UserId == null || request.UserId.Length == 0)
                    throw new ArgumentException("At least one user must be specified.", nameof(request.UserId));

                // Validate all tasks exist
                foreach (var taskId in request.TaskId)
                {
                    var task = await _repositoryTask.GetTaskbyIdAsync(taskId);
                    if (task == null)
                        throw new InvalidOperationException($"Task with ID {taskId} not found.");
                }

                // Validate all users exist
                foreach (var userId in request.UserId)
                {
                    var user = await _repositoryUsers.GetUserByIdAsync(userId);
                    if (user == null)
                        throw new InvalidOperationException($"User with ID {userId} not found.");
                }

                // Assign each task to each user
                foreach (var taskId in request.TaskId)
                {
                    foreach (var userId in request.UserId)
                    {
                        var existingStatus = await _repositoryStatus.GetAsync(userId, taskId);
                        if (existingStatus == null)
                        {
                            var status = new StatusTable
                            {
                                UserId = userId,
                                TaskId = taskId,
                                CurrentStatus = TypeofStatus.Pending
                            };
                            await _repositoryStatus.SetStatus(status);
                        }
                    }
                }

                await _repositoryStatus.SaveChangesAsync();
            }
        }
}