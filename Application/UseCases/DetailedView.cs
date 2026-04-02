using TmModule.Modules.Status.Models;
using TmModule.Modules.Status.Repositories;
using TmModule.Modules.Tasks.Repositories;
using TmModule.Modules.Users.Repositories;

namespace TmModule.Application.UseCases
{
        public class DetailedView(IRepositoryStatus repositoryStatus,
                                   IRepositoryUsers repositoryUsers,
                                   IRepositoryTask repositoryTask)
        {
            private readonly IRepositoryStatus _repositoryStatus = repositoryStatus;
            private readonly IRepositoryUsers _repositoryUsers = repositoryUsers;
            private readonly IRepositoryTask _repositoryTask = repositoryTask;

            public async Task<List<object>> GetAllDetails()
            {
                var allStatuses = await _repositoryStatus.GetAllAsync();

                var result = new List<object>();

                foreach (var status in allStatuses)
                {
                    var user = await _repositoryUsers.GetUserByIdAsync(status.UserId);
                    var task = await _repositoryTask.GetTaskbyIdAsync(status.TaskId);

                    result.Add(new
                    {
                        UserId = user?.UserId,
                        Username = user?.Username,
                        TaskId = task?.TaskId,
                        Description = task?.Description,
                        Status = status.CurrentStatus
                    });
                }

                return result;
            }
        }
    
}