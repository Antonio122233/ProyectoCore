using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.DTOs.Abono
{
    public class AbonoDto
    {
        public int IdAbono { get; set; }

        public int IdVenta { get; set; }

        public decimal MontoAbono { get; set; }

        public int IdTipoPago { get; set; }

        public string? NombreTipoPago { get; set; }

        public DateTime FechaAbono { get; set; }

        public string? Observaciones { get; set; }

        public bool EstadoRegistro { get; set; }

        public decimal NuevoMontoPagado { get; set; }

        public decimal NuevoSaldoPendiente { get; set; }

        public string EstadoPago { get; set; } = string.Empty;
    }
}
