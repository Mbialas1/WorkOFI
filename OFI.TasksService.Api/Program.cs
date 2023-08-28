using Core.InterfaceRepository;
using OFI.Infrastructure.User;
using OFI.Infrastructure.Task;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

var app = builder.Build();

app.Run();