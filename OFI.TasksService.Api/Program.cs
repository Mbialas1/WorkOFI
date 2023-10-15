using Core.Application.Commands;
using Core.InterfaceRepository;
using Core.Services.InterfaceServices;
using Core.Services.Services;
using Microsoft.AspNetCore.Hosting;
using OFI.Infrastructure.Task;
using MediatR;
using Core.Entities.Task;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Serilog;
using Serilog.Events;
using OFI.Infrastructure.Handlers.Tasks.Commands;
using Core.Application.Services;
using Core.Application.Services.Helpers;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();


// Add services to the container.
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddControllers();
builder.Services.AddTransient<AddTaskCommandHandler, AddTaskCommandHandler>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AddTaskCommandHandler).Assembly);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", builder =>
    {
        builder.WithOrigins("http://localhost:8082", "http://localhost:8080")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddHttpClient<IUserService, UserCommunicationService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration[ServicesHelper.User_api_services_configuration]!);
});


var app = builder.Build();
app.UseCors("AllowAngularApp");
app.UseRouting();
app.MapControllers();

app.Run();
