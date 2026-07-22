using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.DTOs.Compra
{
    public class DetalleCompraDto
    {
        public int IdProducto { get; set; }
        public decimal PrecioCompra { get; set; }
        public int Cantidad { get; set; }
        public decimal? Subtotal { get; set; }
        public bool EstadoRegistro { get; set; }
    }
}
