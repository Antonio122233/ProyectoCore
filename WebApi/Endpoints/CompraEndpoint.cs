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
            return app;
        }
    }
}
