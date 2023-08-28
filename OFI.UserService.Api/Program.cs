using Core.InterfaceRepository;
using OFI.Infrastructure.Task;
using OFI.Infrastructure.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

app.Run();