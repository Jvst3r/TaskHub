namespace Api.Controllers.Tasks.Response
{
    public sealed class TaskResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
