namespace Aplicacion.DTOs.Venta
{
    public class VentaCreateDto
    {
        public int IdCliente { get; set; }
        public string TipoVenta { get; set; } = string.Empty;
        public int? IdTipoPago { get; set; }
        public string? Observaciones { get; set; }
        public List<DetalleVentaCreateDto> Detalles { get; set; } = new List<DetalleVentaCreateDto>();
    }
}
