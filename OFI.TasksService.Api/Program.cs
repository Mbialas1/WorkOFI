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
using Serilog;
using Serilog.Events;

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
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});



var app = builder.Build();
app.UseCors("AllowAllOrigins");
app.UseRouting();
app.MapControllers();

app.Run();
