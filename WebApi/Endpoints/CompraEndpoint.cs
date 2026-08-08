using Aplicacion.Common;
using Aplicacion.DTOs.Compra;
using Aplicacion.Interfaces.Servicios;

namespace WebApi.Endpoints
{
    public static class CompraEndpoint
    {
        public static WebApplication MapCompraEndpoints(this WebApplication app)
        {
            var grupo = app.MapGroup("/api/compra")
                .WithTags("Compra");

            //post Crear
            grupo.MapPost("/", async (CompraCreateDto dto, ICompraService service) =>
                {
                    var nueva = await service.CreateAsync(dto);
                    return Results.Created(
                        $"/api/compra/{nueva.IdCompra}", ApiResponse<CompraDto>.Ok
                        (
                            nueva, "Compra creada correctamente"
                        ));
                });

            //obtener compra por id
            grupo.MapGet("/{id:int}", async (int id, ICompraService service) =>
            {
                var compra = await service.GetByIdAsync(id);
                if (compra == null)
                    throw new KeyNotFoundException("La compra no existe");
                return Results.Ok(ApiResponse<CompraDto>.Ok
                    (compra, "Compra encontrada correctamente"));
            });

            //obtener todas las compras
            grupo.MapGet("/", async (ICompraService service) =>
            {
                var compras = await service.ObtenerComprasCompletas();             
                return Results.Ok(ApiResponse<IEnumerable<CompraDto>>.Ok
                  (compras, "Listado de Compras"));
            });

            return app;
        }
    }
}
