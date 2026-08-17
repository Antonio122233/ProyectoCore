using Aplicacion.Common;
using Aplicacion.DTOs.Cliente;
using Aplicacion.Interfaces.Servicios;

namespace WebApi.Endpoints
{
    public static class ClienteEndpoint
    {
        public static WebApplication MapClienteEndpoints(this WebApplication app)
        {
            var grupo = app.MapGroup("/api/cliente")
                .WithTags("Cliente");

            grupo.MapPost("/", async (ClienteCreateDto dto,IClienteService service) 
                =>
            {
                var cliente = await service.CreateAsync(dto);

                return Results.Created($"/api/cliente/{cliente.IdCliente}",
                    ApiResponse<ClienteDto>.Ok(cliente,"Cliente creado correctamente"));
            });

            return app;
        }
    }
}
