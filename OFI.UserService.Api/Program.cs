using Core.InterfaceRepository;
using Core.Services.InterfaceServices;
using Core.Services.Services;
using OFI.Infrastructure.Handlers;
using Serilog.Events;
using Serilog;
using OFI.Infrastructure.User;
using OFI.Infrastructure.Handlers.Users.Queries;
using StackExchange.Redis;
using Core.Application.Services.Helpers;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();


// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers();
builder.Services.AddTransient<GetAllUsersForDashboardQueriesHandler, GetAllUsersForDashboardQueriesHandler>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetAllUsersForDashboardQueriesHandler).Assembly);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", builder =>
    {
        builder.WithOrigins("http://localhost:8082", "http://localhost:8081")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

#region redis
var multiplexer = ConnectionMultiplexer.Connect(builder.Configuration.GetSection(ServicesHelper.Redis_task_services_configuration).Value ?? string.Empty);
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
#endregion

var app = builder.Build();
app.UseCors("AllowAngularApp");
app.UseRouting();
app.MapControllers();

app.Run();