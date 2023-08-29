using Core.Application.Commands;
using Core.InterfaceRepository;
using Core.Services.InterfaceServices;
using Core.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using OFI.Infrastructure.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddMediatR(typeof(CreateUserCommand).Assembly);

app.Run();