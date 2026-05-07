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

        [Theory]
        [InlineData(0,new TaskListResponse(new List<TaskModel> ))]
        [InlineData(5)]
        public async Task GetAllTasksAsyncWhenTask(int count, TaskListResponse expected)
        {

        }


    }
}
