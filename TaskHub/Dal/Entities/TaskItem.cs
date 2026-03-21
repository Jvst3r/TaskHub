using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
