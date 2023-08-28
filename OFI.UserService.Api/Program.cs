using Core.InterfaceRepository;
using Core.Services.InterfaceServices;
using Core.Services.Services;
using OFI.Infrastructure.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

app.Run();