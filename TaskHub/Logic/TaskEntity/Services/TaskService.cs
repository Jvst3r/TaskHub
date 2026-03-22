using Dal.Entities;
using Dal.Repositories;
using Dal.Repositories.Interfaces;
using Logic.TaskEntity.Models;
using Logic.TaskEntity.Services.Interfaces;
using System.Linq;
using Dal.Entities;
namespace Logic.TaskEntity.Services;

public sealed class TaskService : ITaskService
{
    private readonly ITaskRepository taskRepository;

    public TaskService(ITaskRepository _taskRepository)
    {
        this.taskRepository = _taskRepository;
    }

    public async Task<TaskModel> CreateTaskAsync(string title, Guid createdByUserId, CancellationToken ct)
    {
        var taskEntity = new Dal.Entities.TaskItem(title, createdByUserId);

        var createdEntity = await taskRepository.CreateTaskAsync(taskEntity, ct);

        return new TaskModel(createdEntity);
    }

    public async Task DeleteAllTasksAsync(CancellationToken ct)
    {
        await taskRepository.DeleteAllTasksAsync(ct);
    }

    public async Task<bool> DeleteTaskAsync(Guid id, CancellationToken ct)
    {
        return await taskRepository.DeleteTaskAsync(id, ct);
    }

    public async Task<List<TaskModel>> GetAllTasksAsync(CancellationToken ct)
    {
        var entities = await taskRepository.GetAllTasksAsync(ct);
        return entities.Select(e => new TaskModel(e)).ToList();
    }

    public async Task<TaskModel?> GetTaskByIdAsync(Guid id, CancellationToken ct)
    {
        var entity = await taskRepository.GetTaskByIdAsync(id, ct);
        return entity is null ? null : new TaskModel(entity);
    }

    public async Task<bool> SetTaskTitleAsync(Guid id, string title, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return false;
        }

        return await taskRepository.UpdateTaskTitleAsync(id, title, ct);
    }
}
