using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
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
        public async Task GetTaskByIdWhenTaskExists() { }
        public async Task GetTaskByIdWhenTaskNotFound() { }
        public async Task GetTaskByIdWhenInvalidGuid() { }
        public async Task CreateTaskWhenValidData() { }
        public async Task CreateTaskWhenTitleEmpty() { }
        public async Task CreateTaskWhenUserIdEmpty() { }
        public async Task RenameTaskWhenValidData() { }
        public async Task RenameTaskWhenTitleEmpty() { }
    }
}
