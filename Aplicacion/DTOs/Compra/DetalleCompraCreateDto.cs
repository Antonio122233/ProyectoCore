using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.DTOs.Compra
{
    public class DetalleCompraCreateDto
    {
        public int IdProducto { get; set; }

        public decimal PrecioCompra { get; set; }

        public int Cantidad { get; set; }
    }
}
