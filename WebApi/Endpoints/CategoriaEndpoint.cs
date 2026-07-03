namespace WebApi.Endpoints
{
    public static class CategoriaEndpoint
    {
        public static WebApplication MapCategoriaEnpoint(this WebApplication app)
        {
            var group = app.MapGroup("/api/catagoria")
                .WithTags("Categoria");

            return app;
        }
    }
}
