using Moq;
using Xunit;
using Dal.Entities;
using Logic.TaskEntity;

namespace TaskHub.Tests
{
    public class TaskServiceTests
    {
        private readonly ITaskService taskService ;
        private readonly Mock<ITaskRepository> taskRepository;
      
        public TaskServiceTests()
        {
            taskRepository = new Mock<ITaskRepository>();
            taskService = taskRepository(taskRepository.Object);
        }

        

    }
}