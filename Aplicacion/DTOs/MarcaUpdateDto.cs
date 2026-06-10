using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.DTOs
{
    public class MarcaUpdateDto
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool EstadoRegistro { get; set; }

    }
}
