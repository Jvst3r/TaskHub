using LoggingLibrary;
using Api.Extensions;    
using Api.Services;
using Microsoft.Extensions.DependencyInjection;
namespace Api;

/// <summary>
/// Точка входа приложения
/// </summary>
public sealed class Program
{
    /// <summary>
    /// Запуск приложения
    /// </summary>
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .UseInfraSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .Build();

        //DI
        Console.WriteLine("Scope2 starting...");
        using (var scope1 = host.Services.CreateAsyncScope())
        {
            var provider = scope1.ServiceProvider;
            provider.ResolveAndCompare<ISingletonService1>();
            provider.ResolveAndCompare<ISingletonService2>();
            provider.ResolveAndCompare<IScopedService1>();
            provider.ResolveAndCompare<IScopedService2>();
            provider.ResolveAndCompare<ITransientService1>();
            provider.ResolveAndCompare<ITransientService2>();
        }

        Console.WriteLine("Scope2 starting...");
        using (var scope2 = host.Services.CreateAsyncScope())
        {
            var provider = scope2.ServiceProvider;
            provider.ResolveAndCompare<ISingletonService1>();
            provider.ResolveAndCompare<ISingletonService2>();
            provider.ResolveAndCompare<IScopedService1>();
            provider.ResolveAndCompare<IScopedService2>();
            provider.ResolveAndCompare<ITransientService1>();
            provider.ResolveAndCompare<ITransientService2>();
        }

        host.Run();
    }
}