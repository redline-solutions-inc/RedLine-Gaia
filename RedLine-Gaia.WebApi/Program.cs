using Microsoft.EntityFrameworkCore;
using multiTenantApp.Middleware;
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

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var betterStackSourceToken = builder.Configuration.GetValue<string>("BetterStack:sourceToken");
var betterStackEndpoint = builder.Configuration.GetValue<string>("BetterStack:betterStackEndpoint");

if (!string.IsNullOrEmpty(betterStackSourceToken) && !string.IsNullOrEmpty(betterStackEndpoint))
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
                .WriteTo.BetterStack(
                    sourceToken: betterStackSourceToken,
                    betterStackEndpoint: betterStackEndpoint
                );
        }
    );
}
else
{
    builder.Host.UseSerilog(
        (context, loggerConfiguration) =>
        {
            loggerConfiguration.MinimumLevel.Information().WriteTo.Console();
        }
    );
}

builder.Services.AddScoped<ICurrentTenantService, CurrentTenantService>();

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(RedLine_Gaia.Application.AssemblyReference.Assembly);
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddDbContext<MasterDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MasterDbConnection"))
);

builder.Services.AddApplicaiton().AddInfrastructure();
builder.Services.AddScoped<TenantActionFilter>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseSerilogRequestLogging();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
