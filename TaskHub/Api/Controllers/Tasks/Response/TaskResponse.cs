namespace Api.Controllers.Tasks.Response

{
    public sealed class TaskResponse
    {
        public TaskResponse() { }
        public TaskResponse(Logic.TaskEntity.Models.TaskModel task)
        {
            this.Id = task.Id;
            Title = task.Title;
            CreatedByUserId = task.CreatedByUserId;
            CreatedUtc = task.CreatedUtc;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
