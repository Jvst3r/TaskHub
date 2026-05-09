namespace Api.Controllers.Tasks.Request
{
    public sealed class CreateTaskRequest
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
    }
}
