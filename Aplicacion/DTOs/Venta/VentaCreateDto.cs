using Dominio.Enums;

namespace Aplicacion.DTOs.Venta
{
    public class VentaCreateDto
    {
        public int IdCliente { get; set; }
        public TipoVenta TipoVenta { get; set; }
        public int? IdTipoPago { get; set; }
        public string? Observaciones { get; set; }
        public List<DetalleVentaCreateDto> Detalles { get; set; } = new List<DetalleVentaCreateDto>();
    }
}
