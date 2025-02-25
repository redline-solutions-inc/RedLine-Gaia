using Microsoft.EntityFrameworkCore;
using RedLine_Gaia.Application;
using RedLine_Gaia.Infrastructure;
using RedLine_Gaia.Infrastructure.Database;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddApplicaiton().AddInfrastructure();
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(RedLine_Gaia.Application.AssemblyReference.Assembly);
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
