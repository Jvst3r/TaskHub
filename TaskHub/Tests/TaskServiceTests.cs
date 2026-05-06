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
        private readonly ITaskService taskService;
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
            var id = Guid.NewGuid();
            taskRepository.Setup(r => r.GetTaskByIdAsync(id, It.IsAny<CancellationToken>()))
                                                                    .ReturnsAsync((TaskItem?)null);

            var result = await taskService.GetTaskByIdAsync(id, CancellationToken.None);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllTasksAsyncWhenNoTasks()
        {
            taskRepository.Setup(r => r.GetAllTasksAsync(It.IsAny<CancellationToken>())).ReturnsAsync(new List<TaskItem>());

            var result = taskService.GetAllTasksAsync(CancellationToken.None);

            Assert.NotNull(result);

            Assert.Equal(result.Result.Count, 0);
        }

        [Fact]
        public async Task GetAllTaskAsyncWhenListHasTasks()
        {
            var expected = new List<TaskItem>
            {
                new TaskItem("first", Guid.NewGuid()),
                new TaskItem("second", Guid.NewGuid()),
                new TaskItem("third", Guid.NewGuid())
            };

            taskRepository.Setup(r => r.GetAllTasksAsync(It.IsAny<CancellationToken>())).ReturnsAsync(expected);

            var result = taskService.GetAllTasksAsync(CancellationToken.None).Result;

            Assert.NotNull(result);

            Assert.Equal(result.Count, 3);
        }

        [Fact]
        public async Task DeelteTaskAsync()
        {
            taskRepository.Setup(r => r.DeleteAllTasksAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var result = taskService.DeleteAllTasksAsync(CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.IsCompleted);
        }

        [Fact]
        public async Task DeleteTaskByIdWhenTaskExists()
        {
            taskRepository.Setup(r => r.DeleteTaskAsync(It.IsAny<Guid>(),It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var result = taskService.DeleteTaskAsync(Guid.NewGuid(), It.IsAny<CancellationToken>()).Result;

            Assert.NotNull(result);
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteTaskByIdWhenTaskDoesntExists()
        {
            taskRepository.Setup(r => r.DeleteTaskAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

            var result = taskService.DeleteTaskAsync(Guid.NewGuid(),CancellationToken.None).Result;

            Assert.NotNull(result);
            Assert.False(result);
        }
        
        public async Task SetTitleAsyncWhenTitleIsValid()
        {
            taskRepository.Setup(r => r.UpdateTaskTitleAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var result = taskService.SetTaskTitleAsync(Guid.NewGuid(), "new title", CancellationToken.None).Result;

            Assert.NotNull(result);
            Assert.True(result);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task SetTitleAsyncWhenTitleIsNotValid(string title)
        {
            taskRepository.Setup(r => r.UpdateTaskTitleAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

            var result = taskService.SetTaskTitleAsync(Guid.NewGuid(), title, CancellationToken.None).Result;

            Assert.NotNull(result);
            Assert.False(result);
        }

        
    }
}