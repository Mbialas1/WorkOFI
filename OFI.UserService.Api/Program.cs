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
using RabbitMQ.Client;
using Core.RabbitMQ;
using OFI.Infrastructure.User.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Core.Security.Settings;
using Core.Security.Generator;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();


// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddSingleton<RabbitMqConsumer>();

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
//var multiplexer = ConnectionMultiplexer.Connect(builder.Configuration.GetSection(ServicesHelper.Redis_task_services_configuration).Value ?? String.Empty);
//builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
#endregion

#region RabbitMQ
builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>(sp =>
{
    return new ConnectionFactory
    {
        HostName = builder.Configuration[ServicesHelper.Rabbit_host_configuration],
        UserName = builder.Configuration[ServicesHelper.Rabbit_user_configuration],
        Password = builder.Configuration[ServicesHelper.Rabbit_password_configuration]
    };
});

builder.Services.AddSingleton<IConnection>(sp =>
{
    var factory = sp.GetRequiredService<IConnectionFactory>();
    return factory.CreateConnection();
});

builder.Services.AddSingleton<IModel>(sp =>
{
    var connection = sp.GetRequiredService<IConnection>();
    return connection.CreateModel();
});
#endregion

#region JWT
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddSingleton<JwtTokenGenerator>();
#endregion

var app = builder.Build();

#region RabbitMQ initialize
var channel = app.Services.GetRequiredService<IModel>();
RabbitMqSetup.Initialize(channel);  // je¿eli RabbitMqSetup tworzy kolejkê, to jest to odpowiednie miejsce
var consumer = app.Services.GetRequiredService<RabbitMqConsumer>();
consumer.InitializeConsumer();
#endregion

app.UseCors("AllowAngularApp");
app.UseRouting();
app.MapControllers();

app.Run();