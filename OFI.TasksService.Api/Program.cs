using Core.Application.Commands;
using Core.InterfaceRepository;
using Core.Services.InterfaceServices;
using Core.Services.Services;
using Microsoft.AspNetCore.Hosting;
using OFI.Infrastructure.Task;
using MediatR;
using Core.Entities.Task;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddControllers();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AddTaskCommand).Assembly);
});

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
