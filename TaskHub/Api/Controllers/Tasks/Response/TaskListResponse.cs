using Api.Controllers.Users.Response;

namespace Api.Controllers.Tasks.Response
{
    public class TaskListResponse
    {
        public IReadOnlyCollection<TaskResponse> TaskList { get; init; }
        public TaskListResponse(IReadOnlyCollection<TaskResponse> list) 
        {
            TaskList = list;
        }
    }
}
