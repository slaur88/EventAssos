
using Microsoft.EntityFrameworkCore;
using EventAssos.API.Extentions;
using EventAssos.API.Scalar;
using EventAssos.Infra;
using EventAssos.Secu;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.ConfigureJwTAuthentication(builder.Configuration);
//builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options => options.AddDocumentTransformer<BearerSecuritySchemeTransformer>());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); //Scalar
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
