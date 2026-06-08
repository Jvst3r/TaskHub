using Api.UseCases.Tasks.Interfaces;
using Logic.TaskEntity.Services.Interfaces;
using Moq;
using Api.UseCases.Tasks;
using Logic.TaskEntity.Models;
using System.Runtime.InteropServices;

namespace Tests.UnitTests
{
    public class ManageTasksUseCaseTests
    {
        private readonly Mock<ITaskService> taskService;
        private readonly IManageTaskUseCase manageTaskUseCase;
        public ManageTasksUseCaseTests()
        {
            taskService = new Mock<ITaskService>();
            manageTaskUseCase = new ManageTaskUseCase(taskService.Object);
        }
        [Fact]
        public async Task CreateTaskAsyncWhenDataIsValid()
        {
            var title = "test";
            var userId = Guid.NewGuid();
            var taskModel = new TaskModel(title, userId);

            taskService.Setup(s => s.CreateTaskAsync(title, userId, It.IsAny<CancellationToken>())).ReturnsAsync(taskModel);

            var result = await manageTaskUseCase.CreateTaskAsync(title, userId, It.IsAny<CancellationToken>());

            Assert.NotNull(result);

            Assert.Equal(title, result.Title);

            Assert.Equal(userId, result.CreatedByUserId);
        }

        [Fact]
        public async Task GetTaskByIdAsyncWhenTaskNotFound()
        {

            var taskId = Guid.NewGuid();
            taskService.Setup(s => s.GetTaskByIdAsync(taskId, It.IsAny<CancellationToken>())).ReturnsAsync((TaskModel?)null);

            var result = await manageTaskUseCase.GetTaskByIdAsync(taskId, CancellationToken.None);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllTasksAsyncWhenTasksExists()
        {
            var tasks = new List<TaskModel>
            {
                new TaskModel("first", Guid.NewGuid()),
                new TaskModel("second", Guid.NewGuid())
            };

            taskService.Setup(s => s.GetAllTasksAsync(It.IsAny<CancellationToken>())).ReturnsAsync(tasks);

            var result = await manageTaskUseCase.GetAllTasksAsync(CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(2, result.TaskList.Count);
        }

        [Fact]
        public async Task DeleteAllTasksAsync()
        {
            taskService.Setup(s => s.DeleteAllTasksAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var result = manageTaskUseCase.DeleteAllTasksAsync(CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteTaskAsyncWhenTaskExists()
        {
            var id = Guid.NewGuid();
            taskService.Setup(s => s.DeleteTaskAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var result = await manageTaskUseCase.DeleteTaskAsync(id, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteTaskAsyncWhenTaskDoesntExist()
        {
            var id = Guid.NewGuid();
            taskService.Setup(s => s.DeleteTaskAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(false);

            var result = await manageTaskUseCase.DeleteTaskAsync(id, CancellationToken.None);

            Assert.NotNull(result);
            Assert.False(result);
        }

        [Fact]
        public async Task SetTitleAsyncWhenDataIsValid()
        {
            var id = Guid.NewGuid();
            var title = "test";

            taskService.Setup(s => s.SetTaskTitleAsync(id, title, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var result = await manageTaskUseCase.SetTaskTitleAsync(id, title, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task SetTaskTitleAsyncWhenDataIsInvalid(string title)
        {
            var id = Guid.NewGuid();

            var result = await manageTaskUseCase.SetTaskTitleAsync(id, title, CancellationToken.None);

            Assert.NotNull(result);
            Assert.False(result);
        }
    }
}
