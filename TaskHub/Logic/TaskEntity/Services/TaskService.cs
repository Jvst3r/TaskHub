using Logic.TaskEntity.Models;
using Logic.TaskEntity.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.TaskEntity.Services
{
    public class TaskService : ITaskService
    {
        public Task<TaskItem> CreateTaskAsync(string title, Guid createdByUserId, CancellationToken ct)
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

        public Task<List<TaskItem>> GetAllTasksAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<TaskItem?> GetTaskByIdAsync(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetTaskTitleAsync(Guid id, string title, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
