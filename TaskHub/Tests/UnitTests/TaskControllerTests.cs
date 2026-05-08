using Api.Controllers.Tasks;
using Api.Controllers.Tasks.Response;
using Api.UseCases.Tasks.Interfaces;
using Logic.TaskEntity.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.UnitTests
{
    public class TaskControllerTests
    {
        private readonly Mock<IManageTaskUseCase> manageTaskUseCase;
        
        private readonly TaskController taskController;

        public TaskControllerTests()
        {
            manageTaskUseCase = new Mock<IManageTaskUseCase>();
            taskController = new TaskController(manageTaskUseCase.Object);
        }

        [Fact]
        public async Task GetAllTasksAsyncWhenNoTasks()
        {
            manageTaskUseCase.Setup(uc => uc
                                        .GetAllTasksAsync(It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(new TaskListResponse(list: new List<TaskResponse>()));

            var result = await taskController.GetAllTasks(CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<TaskListResponse>(result); //новый для меня метод для проверки,
                                                     //приколдес че сказать, я так понял в API-тестах там всё что тестится - это типы значений
            Assert.Equal(0, result.);
        }

        [Fact]
        public async Task GetAllTasksAsyncWhenTasksExists()
        {
            manageTaskUseCase.Setup(uc => uc
                                        .GetAllTasksAsync(It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(new TaskListResponse(list: new List<TaskResponse>
                                            {
                                                new TaskResponse(new TaskModel("test", Guid.NewGuid()))
                                            }));

            var result = await taskController.GetAllTasks(CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<TaskListResponse>(result); 
            Assert.Equal(0, result.TaskList.Count);
            Assert.Equal(StatusCode.Ok)
        }

    }
}
