using Microsoft.EntityFrameworkCore;
using multiTenantApp.Middleware;
using RedLine_Gaia.Application;
using RedLine_Gaia.Domain.Interfaces;
using RedLine_Gaia.Infrastructure;
using RedLine_Gaia.Infrastructure.Database;
using RedLine_Gaia.Infrastructure.Services;
using RedLine_Gaia.WebApi.Middleware;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

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

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
