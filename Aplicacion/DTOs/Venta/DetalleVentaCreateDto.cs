using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.DTOs.Venta
{
    public class DetalleVentaCreateDto
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
    }
}
