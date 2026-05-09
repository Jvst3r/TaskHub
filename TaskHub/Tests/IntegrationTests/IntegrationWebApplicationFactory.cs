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
                var descriptors = services
                    .Where(d =>
                        d.ServiceType == typeof(DbContextOptions<TaskDbContext>) ||
                        d.ServiceType == typeof(TaskDbContext) ||
                        d.ImplementationType == typeof(TaskDbContext))
                    .ToList();

                foreach (var descriptor in descriptors)
                {
                    services.Remove(descriptor);
                }

                services.AddSingleton(provider =>
                {
                    return new DbContextOptionsBuilder<TaskDbContext>()
                        .UseInMemoryDatabase("ForTasksTestDb")
                        .Options;
                });

                services.AddScoped<TaskDbContext>(provider =>
                {
                    var options = provider.GetRequiredService<DbContextOptions<TaskDbContext>>();
                    return new TaskDbContext(options);
                });
            });
        }
    }
}
