using Core.Application.Commands;
using Core.InterfaceRepository;
using Core.Services.InterfaceServices;
using Core.Services.Services;
using Microsoft.AspNetCore.Hosting;
using OFI.Infrastructure.Task;
using MediatR;
using Core.Entities.Task;
using Microsoft.Extensions.DependencyInjection;
using OFI.Infrastructure.Handlers;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddControllers();
builder.Services.AddTransient<AddTaskCommandHandler, AddTaskCommandHandler>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AddTaskCommandHandler).Assembly);
});

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
