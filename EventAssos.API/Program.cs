
using EventAssos.API.Extentions;
using EventAssos.API.Scalar;
using EventAssos.Infra;
using EventAssos.Secu;
using EventAssos.Secu.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.ConfigureJwTAuthentication(builder.Configuration);
builder.Services.AddSecuServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);


builder.Services.AddControllers().AddJsonOptions(options =>
 {
     // Cette ligne ignore les cycles infinis
     options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
 });
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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Remplace "AdminSeed" par le nom exact de ta classe si c'est différent
        await AdminSeed.SeedMadameDupontAsync(services);
    }
    catch (Exception ex)
    {
        // Si ça plante, on veut savoir pourquoi dans la console
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Une erreur est survenue lors du seeding de la base de données.");
    }
}

app.Run();
