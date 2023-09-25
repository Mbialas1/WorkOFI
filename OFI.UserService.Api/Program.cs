using Core.Application.Commands;
using Core.InterfaceRepository;
using Core.Services.InterfaceServices;
using Core.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using OFI.Infrastructure.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddMediatR(typeof(AddTaskCommand).Assembly);

app.Run();