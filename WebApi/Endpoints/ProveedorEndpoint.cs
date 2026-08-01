using Aplicacion.Common;
using Aplicacion.DTOs.Marca;
using Aplicacion.DTOs.Proveedor;
using Aplicacion.Interfaces.Servicios;

namespace WebApi.Endpoints
{
    public static class ProveedorEndpoint
    {

        public static WebApplication MapProveedorEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/proveedor")
                .WithTags("Proveedores");

            //GET  ALL : api/proveedor
            group.MapGet("/", async (IProvedorService service)
                =>
            {
                var proveedores = await service.GetAllAsync();
                return Results.Ok(ApiResponse<IEnumerable<ProveedorDto>>
                    .Ok(proveedores, "Listado de Proveedores"));
            });

            //solo activas
            group.MapGet("/activas", async (IProvedorService service)
                =>
            {
                var proveedores = await service.GetActiveAsync();
                return Results.Ok(ApiResponse<IEnumerable<ProveedorDto>>
                    .Ok(proveedores, "Listado de Proveedores Activos"));
            }
             );

            //Get by Id

            group.MapGet("/{id:int}", async (int id, IProvedorService service) =>
            {
                var proveedor = await service.GetByIdAsync(id);
                if (proveedor == null)
                    return Results.NotFound(ApiResponse<object>.Fail("Proveedor no encontrado"));

                return Results.Ok(ApiResponse<ProveedorDto>.Ok(proveedor, "Proveedor Encontrado"));
            });

            ////  POST (Crear)

            group.MapPost("/", async (ProveedorCreateDto dto, IProvedorService service) =>
            {
                var nuevo = await service.CreateAsync(dto);
                return Results.Created($"/api/proveedor/{nuevo.Id}", ApiResponse<ProveedorDto>.Ok(
                    nuevo, "Proveedor Creado Correctamente"));
            });

            //PUT (Actualizar)

            group.MapPut("/{id:int}", async (int id, ProveedorUpdateDto dto, IProvedorService service) =>

            {
                var actualizado = await service.UpdateAsync(id, dto);

                if (!actualizado)
                    return Results.NotFound(ApiResponse<object>.Fail("Proveedor no encontrada"));

                return Results.NoContent(); // regresa un 204
            });

            //  DELETE (Eliminar)
            group.MapDelete("/{id:int}", async (int id, IProvedorService service) =>
            {
                var eliminado = await service.DeleteAsync(id);

                if (!eliminado)

                    return Results.NotFound(ApiResponse<object>.Fail("Marca no encontrada"));

                return Results.NoContent(); //regresa un 404
            });

            group.MapGet("/nombre/{nombre}", async (string nombre, IProvedorService service) =>
            {
                var proveedor = await service.GetByNombreAsync(nombre);

                if (proveedor == null)
                {
                    return Results.NotFound(ApiResponse<object>.Fail("proveedor no encontrado"));
                }
                return Results.Ok(ApiResponse<ProveedorDto>.Ok(proveedor, "Proveedor encontrado"));
            });

            return app;
        }
    }
}
