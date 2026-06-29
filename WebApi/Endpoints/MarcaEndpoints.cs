using Aplicacion.Common;
using Aplicacion.DTOs.Marca;
using Aplicacion.Interfaces;

namespace WebApi.Endpoints
{
    public static class MarcaEndpoints
    {
        public static WebApplication MapMarcaEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/marca")
                .WithTags("Marca"); ;

            //GET  ALL : api/marca
            group.MapGet("/", async (IMarcaService service)
                =>
            {
                var marcas = await service.GetAllAsync();
                return Results.Ok(ApiResponse<IEnumerable<MarcaDto>>
                    .Ok(marcas, "Listado de Marcas"));
            });

            //activas

            group.MapGet("/activas", async (IMarcaService service)
                 =>
            {
                var marcas =await service.GetActiveAsync();
                return Results.Ok(ApiResponse<IEnumerable<MarcaDto>>
                    .Ok(marcas, "Listado de marcas activas"));
            });


            // GET BY ID
            group.MapGet("/{id:int}", async (int id, IMarcaService service) =>
            {
                var marca = await service.GetByIdAsync(id);

                if (marca == null)
                    return Results.NotFound(ApiResponse<object>.Fail("Marca no encontrada"));

                return Results.Ok(ApiResponse<MarcaDto>
              .Ok(marca, "Marca encontrada"));
            });


            //  POST (Crear)
            group.MapPost("/", async (MarcaCreateDto dto, IMarcaService service) =>
            {
                var nueva = await service.CreateAsync(dto);
                return Results.Created($"/api/marca/{nueva.Id}", ApiResponse<MarcaDto>.Ok(nueva, "Marca creada correctamente"));
            });


            // PUT (Actualizar)
            group.MapPut("/{id:int}", async (int id, MarcaUpdateDto dto, IMarcaService service) =>
            {
                var actualizado = await service.UpdateAsync(id, dto);

                if (!actualizado)
                    return Results.NotFound(ApiResponse<object>.Fail("Marca no encontrada"));

                return Results.NoContent(); // regresa un 204
            });


            //  DELETE (Eliminar)
            group.MapDelete("/{id:int}", async (int id, IMarcaService service) =>
             {
                var eliminado = await service.DeleteAsync(id);

                if (!eliminado)

                    return Results.NotFound(ApiResponse<object>.Fail("Marca no encontrada"));

                return Results.NoContent(); //regresa un 204
            });            
            return app;
        }
    }
}
