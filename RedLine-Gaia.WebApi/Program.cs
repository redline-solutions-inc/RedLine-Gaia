using Microsoft.EntityFrameworkCore;
using RedLine_Gaia.Application;
using RedLine_Gaia.Domain.Interfaces;
using RedLine_Gaia.Infrastructure;
using RedLine_Gaia.Infrastructure.Database;
using RedLine_Gaia.Infrastructure.Services;
using RedLine_Gaia.WebApi.Middleware;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Core;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);
ConfigureLogging(builder);
ConfigureDatabases(builder);

var app = builder.Build();

ConfigureMiddleware(app, builder);
ConfigureEndpoints(app);

app.Run();

/// <summary>
/// Configures all application services and dependencies.
/// </summary>
static void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddOpenApi();

    builder.Services.AddScoped<ICurrentTenantService, CurrentTenantService>();
    builder.Services.AddScoped<TenantActionFilter>();

    builder.Services.AddMediatR(configuration =>
    {
        configuration.RegisterServicesFromAssembly(
            RedLine_Gaia.Application.AssemblyReference.Assembly
        );
    });

    builder.Services.AddApplicaiton().AddInfrastructure();
}

/// <summary>
/// Configures database contexts.
/// </summary>
static void ConfigureDatabases(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

    builder.Services.AddDbContext<MasterDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("MasterDbConnection"))
    );
}

/// <summary>
/// Configures application logging.
/// </summary>
static void ConfigureLogging(WebApplicationBuilder builder)
{
    var betterStackSourceToken = builder.Configuration.GetValue<string>("BetterStack:sourceToken");
    var betterStackEndpoint = builder.Configuration.GetValue<string>(
        "BetterStack:betterStackEndpoint"
    );

    if (!string.IsNullOrEmpty(betterStackSourceToken) && !string.IsNullOrEmpty(betterStackEndpoint))
    {
        ConfigureBetterStackLogging(builder, betterStackSourceToken, betterStackEndpoint);
    }
    else
    {
        ConfigureConsoleLogging(builder);
    }
}

/// <summary>
/// Configures BetterStack logging.
/// </summary>
static void ConfigureBetterStackLogging(
    WebApplicationBuilder builder,
    string sourceToken,
    string endpoint
)
{
    var levelSwitch = new LoggingLevelSwitch();

    LogEventLevel minimumLogLevel = Enum.TryParse(
        typeof(LogEventLevel),
        builder.Configuration.GetValue("BetterStack:logLevel", "Information"),
        out object? logLevel
    )
        ? (LogEventLevel)logLevel!
        : LogEventLevel.Information;

    levelSwitch.MinimumLevel = minimumLogLevel;

    builder.Host.UseSerilog(
        (context, loggerConfiguration) =>
        {
            loggerConfiguration
                .MinimumLevel.ControlledBy(levelSwitch)
                .WriteTo.BetterStack(sourceToken: sourceToken, betterStackEndpoint: endpoint);
        }
    );
}

/// <summary>
/// Configures console logging for development.
/// </summary>
static void ConfigureConsoleLogging(WebApplicationBuilder builder)
{
    builder.Host.UseSerilog(
        (context, loggerConfiguration) =>
        {
            loggerConfiguration.MinimumLevel.Information().WriteTo.Console();
        }
    );
}

/// <summary>
/// Configures the middleware pipeline.
/// </summary>
static void ConfigureMiddleware(WebApplication app, WebApplicationBuilder builder)
{
    if (app.Environment.IsDevelopment())
    {
        app.MapScalarApiReference();
        app.MapOpenApi();
    }

    app.UseSerilogRequestLogging();
    app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    app.UseHttpsRedirection();
    app.UseAuthorization();
}

/// <summary>
/// Configures endpoints.
/// </summary>
static void ConfigureEndpoints(WebApplication app)
{
    app.MapControllers();
}
