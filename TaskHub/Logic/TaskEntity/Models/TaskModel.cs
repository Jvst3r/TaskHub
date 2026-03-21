using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.TaskEntity.Models
{
    public class TaskModel
    {
        public TaskModel(string title, Guid user)
        {
            Title = title;
            CreatedByUserId = user;
        }

        public TaskModel(Dal.Entities.TaskItem taskItem)
        {
            this.Id = taskItem.Id;
            this.Title = taskItem.Title;
            this.CreatedByUserId = taskItem.CreatedByUserId;
            this.CreatedUtc = taskItem.CreatedUtc;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
