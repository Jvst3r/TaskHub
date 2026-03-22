using Dal.Context;
using Dal.Entities;
using Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext db;

        public TaskRepository(TaskDbContext _db)
        {
            this.db = _db;
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItem task, CancellationToken ct)
        {
            await db.Tasks.AddAsync(task, ct);
            await db.SaveChangesAsync(ct);
            return task;
        }

        public async Task DeleteAllTasksAsync(CancellationToken ct)
        {
            var tasks = await GetAllTasksAsync(ct);
            db.Tasks.RemoveRange(tasks);
            await db.SaveChangesAsync(ct);
        }

        public async Task<bool> DeleteTaskAsync(Guid id, CancellationToken ct)
        {
            var task = await GetTaskByIdAsync(id, ct);
            if (task is null)
            {
                return false;
            }

            db.Tasks.Remove(task);
            await db.SaveChangesAsync(ct);
            return true;
        }

        public async Task<List<TaskItem>> GetAllTasksAsync(CancellationToken ct)
        {
            return await db.Tasks.ToListAsync(ct);
        }

        public async Task<TaskItem?> GetTaskByIdAsync(Guid id, CancellationToken ct)
        {
            return await db.Tasks.FirstOrDefaultAsync(t => t.Id == id, ct);
        }

        public async Task<bool> UpdateTaskTitleAsync(Guid id, string title, CancellationToken ct)
        {
            var task = await GetTaskByIdAsync(id, ct);
            if (task is null)
            {
                return false;
            }

            task.Title = title;

            await db.SaveChangesAsync(ct);
            return true;
        }
    }
}