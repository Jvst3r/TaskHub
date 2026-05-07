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

            var result = await manageTaskUseCase.CreateTaskAsync(title,userId, It.IsAny<CancellationToken>());

            Assert.NotNull(result);

            Assert.Equal(title, result.Title);

            Assert.Equal(userId, result.Id);
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
    }
}
