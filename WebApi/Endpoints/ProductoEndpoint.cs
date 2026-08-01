using Aplicacion.Common;
using Aplicacion.DTOs.Categoria;
using Aplicacion.DTOs.Marca;
using Aplicacion.DTOs.Producto;
using Aplicacion.DTOs.TipoPago;
using Aplicacion.Interfaces.Servicios;

namespace WebApi.Endpoints
{
    public static class ProductoEndpoint
    {
        public static WebApplication MapProductoEnpoint(this WebApplication app)
        {
            var group = app.MapGroup("/api/producto")
               .WithTags("Producto");


            //get by nombre
            group.MapGet("/nombre/{nombre}", async (string nombre, IProductoService service) =>
            {
                var producto = await service.GetByNombreAsync(nombre);

                if (producto == null)
                {
                    return Results.NotFound(ApiResponse<object>.Fail("Producto no encontrado"));
                }
                return Results.Ok(ApiResponse<ProductoDto>.Ok(producto, "Producto encontrado"));
            });


            //activos
            group.MapGet("/activas", async (IProductoService service)
                =>
            {
                var productos = await service.GetActiveAsync();
                return Results.Ok(ApiResponse<IEnumerable<ProductoDto>>
                    .Ok(productos, "Listado de productos activos"));
            });


            // GET BY ID
            group.MapGet("/{id:int}", async (int id, IProductoService service) =>
            {
                var producto = await service.GetByIdAsync(id);

                if (producto == null)
                    return Results.NotFound(ApiResponse<object>.Fail("producto no encontrado"));

                return Results.Ok(ApiResponse<ProductoDto>
              .Ok(producto, "Producto encontrado"));
            });


            //get by categoria
            group.MapGet("/categoria/{id:int}", async (int id, IProductoService service) =>
            {
                var producto = await service.GetByCategoriaAsync(id);

                if (producto == null)
                    return Results.NotFound(ApiResponse<object>.Fail("producto no encontrado"));

                return Results.Ok(ApiResponse<ProductoDto>
              .Ok(producto, "Producto encontrado"));
            });


            //get by marca
            group.MapGet("/marca/{id:int}", async (int id, IProductoService service) =>
            {
                var producto = await service.GetByMarcaAsync(id);

                if (producto == null)
                    return Results.NotFound(ApiResponse<object>.Fail("producto no encontrado"));

                return Results.Ok(ApiResponse<ProductoDto>
              .Ok(producto, "Producto encontrado"));
            });


            //con stock bajo
            group.MapGet("/stockbajo", async (IProductoService service)
                =>
            {
                var productos = await service.ObtenerProductosStockBajoAsync();
                return Results.Ok(ApiResponse<IEnumerable<ProductoDto>>
                    .Ok(productos, "Listado de productos con stock bajo activos"));
            });


            //obtener por codigo de producto
            group.MapGet("/codigoproducto/{codigoproducto}", async (string nombre, IProductoService service) =>
            {
                var producto = await service.SearchByCodigoAsync(nombre);

                if (producto == null)
                {
                    return Results.NotFound(ApiResponse<object>.Fail("Producto no encontrado"));
                }
                return Results.Ok(ApiResponse<ProductoDto>.Ok(producto, "Producto encontrado"));
            });



            ////  POST (Crear)
            group.MapPost("/", async (ProductoCreateDto dto, IProductoService service) =>
            {
                var nuevo = await service.CreateAsync(dto);
                return Results.Created($"/api/producto/{nuevo.IdProducto}", ApiResponse<ProductoDto>.Ok(
                    nuevo, "Producto Creado Correctamente"));
            });



            //PUT (Actualizar)

            group.MapPut("/{id:int}", async (int id, ProductoUpdateDto dto, IProductoService service) =>

            {
                var actualizado = await service.UpdateAsync(id, dto);

                if (!actualizado)
                    return Results.NotFound(ApiResponse<object>.Fail("producto no encontrado"));
                return Results.NoContent(); // regresa un 204
            });

            return app;
        }

    }
}
