using Aplicacion.Common;
using Aplicacion.DTOs.Categoria;
using Aplicacion.DTOs.Proveedor;
using Aplicacion.DTOs.TipoPago;
using Aplicacion.Interfaces;

namespace WebApi.Endpoints
{
    public static class TipoPagoEndpoint
    {
        public static WebApplication MapTipoPagoEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/tipopago")
                .WithTags("tiposPago");

            //GET  ALL : api/tipopago
            group.MapGet("/", async (ITipoPagoService service)
                =>
            {
                var tiposPago = await service.GetAllAsync();
                return Results.Ok(ApiResponse<IEnumerable<TipoPagoDto>>
                    .Ok(tiposPago, "Listado de Tipos de pago"));
            });

            //solo activas
            group.MapGet("/activas", async (ITipoPagoService service)
                =>
            {
                var tiposPago = await service.GetActiveAsync();
                return Results.Ok(ApiResponse<IEnumerable<TipoPagoDto>>
                    .Ok(tiposPago, "Listado de tipos de pago Activos"));
            }
             );

            //Get by Id

            group.MapGet("/{id:int}", async (int id, ITipoPagoService service) =>
            {
                var proveedor = await service.GetByIdAsync(id);
                if (proveedor == null)
                    return Results.NotFound(ApiResponse<object>.Fail("Proveedor no encontrado"));

                return Results.Ok(ApiResponse<TipoPagoDto>.Ok(proveedor, "Proveedor Encontrado"));
            });

            ////  POST (Crear)

            group.MapPost("/", async (TipoPagoCreateDto dto, ITipoPagoService service) =>
            {
                var nuevo = await service.CreateAsync(dto);
                return Results.Created($"/api/proveedor/{nuevo.Id}", ApiResponse<TipoPagoDto>.Ok(
                    nuevo, "Proveedor Creado Correctamente"));
            });

            //PUT (Actualizar)

            group.MapPut("/{id:int}", async (int id, TipoPagoUpdateDto dto, ITipoPagoService service) =>

            {
                var actualizado = await service.UpdateAsync(id, dto);

                if (!actualizado)
                    return Results.NotFound(ApiResponse<object>.Fail("tipo de pago no encontrado"));

                return Results.NoContent(); // regresa un 204
            });

            //  DELETE (Eliminar)
            group.MapDelete("/{id:int}", async (int id, ITipoPagoService service) =>
            {
                var eliminado = await service.DeleteAsync(id);

                if (!eliminado)

                    return Results.NotFound(ApiResponse<object>.Fail("Marca no encontrada"));

                return Results.NoContent(); //regresa un 404
            });

            group.MapGet("/nombre/{nombre}", async (string nombre, ITipoPagoService service) =>
            {
                var categoria = await service.GetByNombreAsync(nombre);

                if (categoria == null)
                {
                    return Results.NotFound(ApiResponse<object>.Fail("Tipo de pago no encontrado"));
                }
                return Results.Ok(ApiResponse<TipoPagoDto>.Ok(categoria, "Tipo de pag encontrado"));
            });

            return app;
        }
    }
}
