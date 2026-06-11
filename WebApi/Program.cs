using Aplicacion;
using Infraestructura;
using WebApi.Endpoints;


var builder = WebApplication.CreateBuilder(args);

//validamos que exista la cadena de conexion
var connectionString = builder.Configuration.GetConnectionString("BdTienda");
if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("Cadena de conexión 'BdTienda' no encontrada.");

// Agregar Infraestructura con la cadena de conexión
builder.Services.
    AddApplicationServices().
    AddInfrastructure(
    builder.Configuration.GetConnectionString("BdTienda")!
);
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/", () => "API funcionando");


//Endpoints
app.MapMarcaEndpoints();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
