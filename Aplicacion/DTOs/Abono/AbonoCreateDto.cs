using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.DTOs.Abono
{
    public class AbonoCreateDto
    {
        public int IdVenta { get; set; }
        public decimal MontoAbono { get; set; }
        public int IdTipoPago { get; set; }
        public string? Observaciones { get; set; }
    }
}
