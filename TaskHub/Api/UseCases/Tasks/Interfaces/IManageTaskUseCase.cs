using Api.Controllers.Tasks.Response;

namespace Api.UseCases.Tasks.Interfaces
{
    public interface IManageTaskUseCase
    {
        Task<TaskResponse> CreateTaskAsync(string title, Guid createdByUserId, CancellationToken ct);
        Task<TaskListResponse> GetAllTasksAsync(CancellationToken ct);
        Task<TaskResponse?> GetTaskByIdAsync(Guid id, CancellationToken ct);
        Task<bool> SetTaskTitleAsync(Guid id, string title, CancellationToken ct);
        Task<bool> DeleteTaskAsync(Guid id, CancellationToken ct);
        Task DeleteAllTasksAsync(CancellationToken ct);
    }
}
