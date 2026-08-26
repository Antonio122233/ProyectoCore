using Aplicacion.Common;
using Aplicacion.DTOs.Abono;
using Aplicacion.Interfaces.Servicios;

namespace WebApi.Endpoints
{
    public static class AbonoEndpoints
    {
        public static WebApplication MapAbonoEndpoints(this WebApplication app)
        {
            var grupo = app.MapGroup("/api/abono")
                .WithTags("Abonos");

            grupo.MapPost("/", async (
                AbonoCreateDto dto,
                IAbonoService service) =>
            {
                var abono =
                    await service.CreateAsync(dto);

                return Results.Created(
                    $"/api/abono/{abono.IdAbono}",
                    ApiResponse<AbonoDto>.Ok(abono,"Abono registrado correctamente"));
            });

            return app;
        }
    }
}
