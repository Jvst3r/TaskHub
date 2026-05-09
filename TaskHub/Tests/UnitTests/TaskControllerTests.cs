using Api.Controllers.Tasks;
using Api.Controllers.Tasks.Response;
using Api.UseCases.Tasks.Interfaces;
using Logic.TaskEntity.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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


            var okResult = Assert.IsType<OkObjectResult>(result);//новый для меня метод для проверки,
                                                                 //приколдес че сказать, я так понял в API-тестах там всё что тестится - это типы значений
            var returnValue = Assert.IsType<TaskListResponse>(okResult.Value);

            Assert.Empty(returnValue.TaskList);
            Assert.Equal(200, okResult.StatusCode);
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

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<TaskListResponse>(okResult.Value);

            Assert.Single(returnValue.TaskList); ///еще новый прикол это метод сингл
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task DeleteAllTasksAsync()
        {
            manageTaskUseCase.Setup(uc => uc.DeleteAllTasksAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var result = await taskController.DeleteAllTasksAsync(CancellationToken.None);

            Assert.NotNull(result);
            var actionResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteTaskByIdAsyncWhenTaskExists()
        {
            var id = Guid.NewGuid();
            manageTaskUseCase.Setup(uc => uc.DeleteTaskAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var result = await taskController.DeleteTaskByIdAsync(id, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteTaskByIdAsyncWhenTaskDoesntExist()
        {
            var id = Guid.NewGuid();
            manageTaskUseCase.Setup(uc => uc.DeleteTaskAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(false);

            var result = await taskController.DeleteTaskByIdAsync(id, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

    }
}
