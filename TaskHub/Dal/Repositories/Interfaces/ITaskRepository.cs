using Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem> CreateTaskAsync(TaskItem task, CancellationToken ct);
        Task<List<TaskItem>> GetAllTasksAsync(CancellationToken ct);
        Task<TaskItem?> GetTaskByIdAsync(Guid id, CancellationToken ct);
        Task<bool> UpdateTaskTitleAsync(Guid id, string title, CancellationToken ct);
        Task<bool> DeleteTaskAsync(Guid id, CancellationToken ct);
        Task DeleteAllTasksAsync(CancellationToken ct);
    }
}
