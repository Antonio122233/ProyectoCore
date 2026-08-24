using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Aplicacion.DTOs.Compra
{
    public class CompraCreateDto
    {
        public int IdProveedor { get; set; }
        public int IdTipoPago { get; set; }
        [JsonIgnore]
        public decimal MontoPagado { get; set; } 
        public string? Observaciones { get; set; }
        public DateTime FechaCompra { get; set; } = DateTime.Now;
        public List<DetalleCompraCreateDto> Detalles { get; set; } = new();
        public string? NumFactura { get; set; }
    }


}
