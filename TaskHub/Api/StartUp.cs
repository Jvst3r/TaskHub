using Api.Middleware;
using Api.Services;
using Api.UseCases.Users;
using Api.UseCases.Users.Interfaces;
using Dal;
using Logic;
using Microsoft.OpenApi.Models;

namespace Api;

/// <summary>
/// Конфигурация приложения
/// </summary>
public sealed class Startup
{
    /// <summary>
    /// Конфигурация приложения
    /// </summary>
    private IConfiguration Configuration { get; }

    /// <summary>
    /// Окружение приложения
    /// </summary>
    private IWebHostEnvironment Environment { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Environment = env;
    }

    /// <summary>
    /// Регистрация сервисов
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddDal();
        services.AddLogic();
        
        services.AddScoped<IManageUserUseCase, ManageUserUseCase>();

        //регистрация сервисов для выполнения задания по DI
        services.AddSingleton<ISingletonService1, SingletonService1>();
        services.AddSingleton<ISingletonService2, SingletonService2>();
        services.AddScoped<IScopedService1, ScopedService1>();
        services.AddScoped<IScopedService2, ScopedService2>();
        services.AddTransient<ITransientService1, TransientService1>();
        services.AddTransient<ITransientService2, TransientService2>();


        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "TaskHub Api",
                Version = "v1"
            });
        });
    }

    /// <summary>
    /// Конфигурация middleware пайплайна
    /// </summary>
    /// <param name="app">Построитель приложения</param>
    public void Configure(IApplicationBuilder app)
    {
        if (Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskHub API v1");
            });
        }

        app.UseRouting();

        //Созданные middleware`ы
        app.UseMiddleware<ResponseTimeMiddleware>();
        app.UseMiddleware<StudentInfoMiddleware>();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}