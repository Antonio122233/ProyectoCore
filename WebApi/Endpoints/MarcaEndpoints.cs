using Aplicacion.Interfaces;

namespace WebApi.Endpoints
{
    public static class MarcaEndpoints
    {
        public static WebApplication MapMarcaEndpoints (this WebApplication app)
        {
            var group = app.MapGroup("/api/marca");

            //GET  ALL : api/marca
            group.MapGet("/", async (IMarcaService service)
                =>
            {
                var marcas = await service.GetAllAsync();
                return Results.Ok(marcas);
            });



            return app;
        }
    }
}
