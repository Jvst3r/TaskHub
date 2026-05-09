using Api.Controllers.Tasks.Request;
using Api.Controllers.Tasks.Response;
using Logic.TaskEntity.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace Tests.IntegrationTests
{
    public class TaskControllerIntegrationTests : IClassFixture<IntegrationWebApplicationFactory>
    {
        private readonly HttpClient client;

        public TaskControllerIntegrationTests(IntegrationWebApplicationFactory factory)
        {
            client = factory.CreateClient();
        }


        [Fact]
        public async Task GetAllTasksWhenSuccess()
        {
            var response = await client.GetAsync("/tasks");

            Assert.NotNull(response);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetTaskByIdWhenTaskExists() 
        {
            var createRequest = new CreateTaskRequest
            {
                Title = "test",
                UserId = Guid.NewGuid()
            };

            var createResponse = await client.PostAsJsonAsync("/tasks", createRequest);

            createResponse.EnsureSuccessStatusCode();

            var createdTask = await createResponse.Content.ReadFromJsonAsync<TaskResponse>();
            Assert.NotNull(createdTask);

            var response = await client.GetAsync($"/tasks/{createdTask.Id}");

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var task = await response.Content.ReadFromJsonAsync<TaskResponse>();

            Assert.Equal(createRequest.Title, task.Title);
        }

        [Fact]
        public async Task GetTaskByIdWhenTaskNotFound() 
        {
            var notExistedTaskId = Guid.NewGuid();

            var response = await client.GetAsync($"/tasks/{notExistedTaskId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetTaskByIdWhenInvalidGuid() 
        {
            var wrongId = "gg_wrong_test";

            var response = await client.GetAsync($"tasks/wrongId");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateTaskWhenValidData() 
        {
            var userId = Guid.Parse("62fd3021-9f6a-44df-8156-2062aa77607c"); //отрывок хардкода из TaskController, решил там ничего не менять
            var title = "возьмите меня на практику пожалуйста:)";
            var request = new CreateTaskRequest { Title = title, UserId = userId };
            var response = await client.PostAsJsonAsync("/tasks", request);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var task = await response.Content.ReadFromJsonAsync<TaskResponse>();

            Assert.NotNull(task);
            Assert.Equal(request.Title, task.Title);
            Assert.Equal(request.UserId, task.CreatedByUserId);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("    ")]
        public async Task CreateTaskWhenTitleEmpty(string title) 
        {
            var userId = Guid.Parse("62fd3021-9f6a-44df-8156-2062aa77607c");
            var request = new CreateTaskRequest { Title = title, UserId = userId };
            var response = await client.PostAsJsonAsync("/tasks", request);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        public async Task CreateTaskWhenUserIdEmpty() { }
        public async Task RenameTaskWhenValidData() { }
        public async Task RenameTaskWhenTitleEmpty() { }
    }
}
