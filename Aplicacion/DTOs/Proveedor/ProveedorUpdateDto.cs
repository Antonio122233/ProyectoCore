using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.DTOs.Proveedor
{
    public class ProveedorUpdateDto
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool EstadoRegistro { get; set; }
    }
}
