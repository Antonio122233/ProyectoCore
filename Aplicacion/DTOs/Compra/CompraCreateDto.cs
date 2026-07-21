using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.DTOs.Compra
{
    public class CompraCreateDto
    {
        public int IdProveedor { get; set; }
        public int IdTipoPago { get; set; }
        public int MontoPagado { get; set; }
        public string? Observaciones { get; set; }
        public DateTime FechaCompra { get; set; } = DateTime.Now;
        public List<DetalleCompraDto> Detalles { get; set; }
    }


}
