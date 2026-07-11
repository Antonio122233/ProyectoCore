using Aplicacion.Common;
using Aplicacion.DTOs.Categoria;
using Aplicacion.DTOs.Marca;
using Aplicacion.Interfaces;

namespace WebApi.Endpoints
{
    public static class CategoriaEndpoint
    {
        public static WebApplication MapCategoriaEnpoint(this WebApplication app)
        {
            var group = app.MapGroup("/api/categoria")
                .WithTags("Categoria");

            //GET  ALL : api/categoria
            group.MapGet("/", async (ICategoriaService service)
                =>
            {
                var categorias = await service.GetAllAsync();
                return Results.Ok(ApiResponse<IEnumerable<CategoriaDto>>
                    .Ok(categorias, "Listado de Categorias"));
            });

            //activas

            group.MapGet("/activas", async (ICategoriaService service)
                 =>
            {
                var categorias = await service.GetActiveAsync();
                return Results.Ok(ApiResponse<IEnumerable<CategoriaDto>>
                    .Ok(categorias, "Listado de categorias activas"));
            });


            // GET BY ID
            group.MapGet("/{id:int}", async (int id, ICategoriaService service) =>
            {
                var categoria = await service.GetByIdAsync(id);

                if (categoria == null)
                    return Results.NotFound(ApiResponse<object>.Fail("Categoria no encontrada"));

                return Results.Ok(ApiResponse<CategoriaDto>
              .Ok(categoria, "Categoria encontrada"));
            });


            //  POST (Crear)
            group.MapPost("/", async (CategoriaCreateDto dto, ICategoriaService service) =>
            {
                var nueva = await service.CreateAsync(dto);
                return Results.Created($"/api/categoria/{nueva.IdCategoria}", ApiResponse<CategoriaDto>.Ok(nueva, "Categoria creada correctamente"));
            });


            // PUT (Actualizar)
            group.MapPut("/{id:int}", async (int id, CategoriaUpdateDto dto, ICategoriaService service) =>
            {
                var actualizado = await service.UpdateAsync(id, dto);

                if (!actualizado)
                    return Results.NotFound(ApiResponse<object>.Fail("Categoria no encontrada"));

                return Results.NoContent(); // regresa un 204
            });


            //  DELETE (Eliminar)
            group.MapDelete("/{id:int}", async (int id, ICategoriaService service) =>
            {
                var eliminado = await service.DeleteAsync(id);

                if (!eliminado)

                    return Results.NotFound(ApiResponse<object>.Fail("Categoria no encontrada"));

                return Results.NoContent(); //regresa un 204
            });


            group.MapGet("/nombre/{nombre}", async (string nombre, ICategoriaService service) =>
            {
                var categoria = await service.GetByNombreAsync(nombre);

                if (categoria == null)
                {
                    return Results.NotFound(ApiResponse<object>.Fail("Categoria no encontrada"));
                }
                return Results.Ok(ApiResponse<CategoriaDto>.Ok(categoria, "Categoria encontrada"));
            });



            return app;
        }
    }
}
