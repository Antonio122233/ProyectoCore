using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.DTOs.Venta
{
    public class VentaDto
    {
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public string? NombreCliente { get; set; }
        public int? IdTipoPago { get; set; }
        public string? NombreTipoPago { get; set; }
        public string? TipoVenta { get; set; }
        public decimal TotalVenta { get; set; }
        public decimal MontoPagado { get; set; }
        public DateTime FechaVenta { get; set; }
        public bool EstadoRegistro { get; set; }
        public List<DetalleVentaDto> Detalles { get; set; } = new();
    }
}
