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

            return app;
        }
    }
}
