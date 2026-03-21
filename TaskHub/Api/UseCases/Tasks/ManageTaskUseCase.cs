using Api.Controllers.Tasks.Response;
using Api.UseCases.Tasks.Interfaces;

namespace Api.UseCases.Tasks
{
    public class ManageTaskUseCase : IManageTaskUseCase
    {
        public ManageTaskUseCase() 
        {

        }

        public Task<TaskResponse> CreateTaskAsync(string title, Guid createdByUserId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllTasksAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTaskAsync(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaskResponse>> GetAllTasksAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<TaskResponse?> GetTaskByIdAsync(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetTaskTitleAsync(Guid id, string title, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
