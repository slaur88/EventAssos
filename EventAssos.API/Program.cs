
using EventAssos.Core;
using EventAssos.Infra;
using Scalar.AspNetCore;
using EventAssos.API.Extentions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureJwTAuthentication(builder.Configuration);
builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); //Scalar
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
