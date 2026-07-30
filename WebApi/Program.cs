using Aplicacion;
using Infraestructura;
using WebApi.Endpoints;
using WebApi.Middlewares;


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

//agregamos los middleware antes de los endpoints
app.UseMiddleware<ExceptionHandlingMiddleware>();

//Endpoints
app.MapMarcaEndpoints();
app.MapProveedorEndpoints();
app.MapTipoPagoEndpoints();
app.MapCategoriaEnpoint();
app.MapProductoEnpoint();

app.Run();
