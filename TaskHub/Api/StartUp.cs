using Api.Middleware;
using Api.UseCases.Tasks;
using Api.UseCases.Tasks.Interfaces;
using Api.UseCases.Users;
using Api.UseCases.Users.Interfaces;
using Dal;
using Dal.Context;
using Dal.Repositories;
using Dal.Repositories.Interfaces;
using Logic;
using Logic.TaskEntity.Services;
using Logic.TaskEntity.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using DatabaseLibrary;
using Api.Attributes.ModelBinders;
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

        //добавлено для задания TaskController
        services.AddDatabase<TaskDbContext>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IManageTaskUseCase, ManageTaskUseCase>();

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