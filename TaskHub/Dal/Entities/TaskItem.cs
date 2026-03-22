using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Entities
{
    public class TaskItem
    {
        //public TaskItem() 
        //{
        //    Id = Guid.NewGuid();
        //    CreatedUtc = DateTimeOffset.UtcNow;
        //}

        public TaskItem() { }

        public TaskItem(string title, Guid user)
        {
            Id = Guid.NewGuid();
            Title = title;
            CreatedByUserId = user;
            CreatedUtc = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
