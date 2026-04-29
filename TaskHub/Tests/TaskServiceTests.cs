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
            var userId = new Guid();

            var createdModel = new TaskModel(title, userId);

            taskRepository
                .Setup(r => r.CreateTaskAsync
                (It.IsAny<TaskItem>(), 
                It.IsAny<CancellationToken>()));

            var result = await taskService.CreateTaskAsync(title, userId, CancellationToken.None);

            Assert.NotNull(result);

            Assert.Equal(createdModel, result);

            Assert.Equal(createdModel.Id, result.Id);

        }

    }
}