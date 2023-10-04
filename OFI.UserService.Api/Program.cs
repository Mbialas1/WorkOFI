using Core.InterfaceRepository;
using Core.Services.InterfaceServices;
using Core.Services.Services;
using OFI.Infrastructure.Handlers;
using Serilog.Events;
using Serilog;
using OFI.Infrastructure.User;
using OFI.Infrastructure.Handlers.Users.Queries;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();


// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
builder.Services.AddTransient<GetAllUsersForDashboardQueriesHandler, GetAllUsersForDashboardQueriesHandler>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetAllUsersForDashboardQueriesHandler).Assembly);
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