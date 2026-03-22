using Api.Controllers.Tasks.Response;
using Api.UseCases.Tasks.Interfaces;
using Logic.TaskEntity.Services;
using Logic.TaskEntity.Services.Interfaces;

namespace Api.UseCases.Tasks
{
    internal class ManageTaskUseCase : IManageTaskUseCase
    {
        private readonly ITaskService taskService;
        public ManageTaskUseCase(ITaskService _taskService) 
        {
            taskService = _taskService;
        }

        public async Task<TaskResponse> CreateTaskAsync(string title, Guid createdByUserId, CancellationToken ct)
        {
            var task = await taskService.CreateTaskAsync(title, createdByUserId, ct);
            return new TaskResponse(task);
        }

        public async Task DeleteAllTasksAsync(CancellationToken ct)
        {
            await taskService.DeleteAllTasksAsync(ct);
        }

        public async Task<bool> DeleteTaskAsync(Guid id, CancellationToken ct)
        {
            return await taskService.DeleteTaskAsync(id, ct);
        }

        public async Task<TaskListResponse> GetAllTasksAsync(CancellationToken ct)
        {
            var tasks = await taskService.GetAllTasksAsync(ct);
            return new TaskListResponse(tasks.Select(t => new TaskResponse(t)).ToList());
        }

        public async Task<TaskResponse?> GetTaskByIdAsync(Guid id, CancellationToken ct)
        {
            var task = await taskService.GetTaskByIdAsync(id, ct);
            return task is null ? null : new TaskResponse(task);
        }

        public async Task<bool> SetTaskTitleAsync(Guid id, string title, CancellationToken ct)
        {
            return await taskService.SetTaskTitleAsync(id, title, ct);
        }
    }
}
