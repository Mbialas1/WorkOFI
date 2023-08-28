using Core.InterfaceRepository;
using Core.Services.InterfaceServices;
using Core.Services.Services;
using OFI.Infrastructure.Task;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

app.Run();