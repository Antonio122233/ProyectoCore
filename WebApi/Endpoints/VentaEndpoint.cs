using Aplicacion.Common;
using Aplicacion.DTOs.Venta;
using Aplicacion.Interfaces.Servicios;
using System.Runtime.CompilerServices;

namespace WebApi.Endpoints
{
    public static class VentaEndpoint
    {
        public static WebApplication MapVentaEndpoint(this WebApplication app)
        {
            var grupo = app.MapGroup("/api/venta")
               .WithTags("Venta");

            // Crear venta
            grupo.MapPost("/", async (VentaCreateDto dto,IVentaService service) 
                =>
            {
                var venta = await service.CreateAsync(dto);

                return Results.Created($"/api/venta/{venta.IdVenta}", ApiResponse<VentaDto>.Ok(venta,"Venta creada correctamente"));
            });



            //obtener todas
            grupo.MapGet("/", async ( IVentaService service) =>
            {
                var ventas =
                    await service.GetAllAsync();

                return Results.Ok(
                    ApiResponse<IEnumerable<VentaDto>>.Ok(
                        ventas,
                        "Listado de ventas"));
            });


            //obtener por id
            grupo.MapGet("/{id:int}", async (int id, IVentaService service) =>
            {
                var venta =
                    await service.GetByIdAsync(id);

                return Results.Ok(
                    ApiResponse<VentaDto>.Ok(
                        venta,
                        "Venta encontrada correctamente"));
            });

            return app;
        }



    }
}
