using Logic.TaskEntity.Services.Interfaces;
using Moq;
using Xunit;
using Dal.Repositories.Interfaces;
using Dal.Entities;
using Dal.Repositories;
using Logic.TaskEntity.Services;
using Logic.TaskEntity.Models;
namespace TaskHub.Tests
{
    public class TaskServiceTests
    {
        private readonly ITaskService taskService ;
        private readonly Mock<ITaskRepository> taskRepository;
      
        public TaskServiceTests()
        {
            taskRepository = new Mock<ITaskRepository>();
            taskService = new TaskService(taskRepository.Object);
        }

        [Fact]
        public async Task CreateTaskAsyncTestWhenDataIsValid()
        {
            var title = "ValidTitle";
            var userId = Guid.NewGuid();

            var createdModel = new TaskItem(title, userId);

            taskRepository
                .Setup(r => r.CreateTaskAsync
                (It.IsAny<TaskItem>(), 
                It.IsAny<CancellationToken>())).ReturnsAsync(createdModel);

            var result = await taskService.CreateTaskAsync(title, userId, CancellationToken.None);

            Assert.NotNull(result);

            Assert.Equal(createdModel.Id, result.Id);

            Assert.Equal(createdModel.Title, result.Title);

        }

        [Fact]
        public async Task GetTaskByIdAsyncWhenTaskIsNotExist()
        {
            var id = Guid.Parse("12345678-1234-1234-12345678");
            taskRepository.Setup(r => r.GetTaskByIdAsync(id, It.IsAny<CancellationToken>()))
                                                                    .ReturnsAsync(new TaskItem());

            var result = taskService.GetTaskByIdAsync(id, CancellationToken.None);

            Assert.Null(result);
        }

    }
}