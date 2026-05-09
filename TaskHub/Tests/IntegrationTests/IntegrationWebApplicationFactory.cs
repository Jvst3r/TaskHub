using Api;
using Dal.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.IntegrationTests
{
    public class IntegrationWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TaskDbContext>));
                
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<TaskDbContext>(options =>
                {
                    options.UseInMemoryDatabase("ForTasksTestDb");
                });
            });
        }
    }
}
