using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.DTOs.TipoPago
{
    public class TipoPagoDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool? EstadoRegistro { get; set; } = false;
    }
}
