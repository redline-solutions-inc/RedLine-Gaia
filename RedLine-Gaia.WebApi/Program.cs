using Microsoft.EntityFrameworkCore;
using RedLine_Gaia.Application;
using RedLine_Gaia.Infrastructure;
using RedLine_Gaia.Infrastructure.Database;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApplicaiton().AddInfrastructure();

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(
        RedLine_Gaia.Application.AssemblyReference.Assembly
    );
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
