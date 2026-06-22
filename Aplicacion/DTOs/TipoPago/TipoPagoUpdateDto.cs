using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.DTOs.TipoPago
{
    public class TipoPagoUpdateDto
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool EstadoRegistro { get; set; }
    }
}
