using Logic.TaskEntity.Models;

namespace Logic.TaskEntity.Services.Interfaces
{
    public interface ITaskService
    {
        Task<TaskItem> CreateTaskAsync(string title, Guid createdByUserId, CancellationToken ct);
        Task<List<TaskItem>> GetAllTasksAsync(CancellationToken ct);
        Task<TaskItem?> GetTaskByIdAsync(Guid id, CancellationToken ct);
        Task<bool> SetTaskTitleAsync(Guid id, string title, CancellationToken ct);
        Task<bool> DeleteTaskAsync(Guid id, CancellationToken ct);
        Task DeleteAllTasksAsync(CancellationToken ct);
    }
}
