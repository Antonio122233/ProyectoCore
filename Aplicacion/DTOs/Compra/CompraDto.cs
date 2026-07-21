using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.DTOs.Compra
{
    public class CompraDto
    {
        public int IdCompra { get; set; }
        public int IdProveedor { get; set; }
        public string? NombreProveedor { get; set; }
        public int IdTipoPago { get; set; }
        public string? NombreTipoPago { get; set; }
        public decimal TotalCompra { get; set; }
        public decimal? MontoPagado { get; set; } = 0;
        public decimal? SaldoPendiente { get; set; }
        public string? Observaciones { get; set; }
        public bool EstadoRegistro { get; set; }
        public List<DetalleCompraDto> Detalles { get; set; } = new();

    }
}
