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
        public async Task GetTaskByIdWhenInvalidGuid() { }
        public async Task CreateTaskWhenValidData() { }
        public async Task CreateTaskWhenTitleEmpty() { }
        public async Task CreateTaskWhenUserIdEmpty() { }
        public async Task RenameTaskWhenValidData() { }
        public async Task RenameTaskWhenTitleEmpty() { }
    }
}
