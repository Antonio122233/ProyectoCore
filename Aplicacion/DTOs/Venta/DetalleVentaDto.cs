using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.DTOs.Venta
{
    public class DetalleVentaDto
    {
        public int IdProducto { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioVenta { get; set; }

        public decimal? SubTotal { get; set; }

        public bool EstadoRegistro { get; set; }
    }
}
