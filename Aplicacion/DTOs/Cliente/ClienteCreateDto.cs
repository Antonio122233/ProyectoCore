using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.DTOs.Cliente
{
    public class ClienteCreateDto
    {
        public string Nombre { get; set; } = string.Empty;

        public string? Apellido { get; set; }

        public string? Telefono { get; set; }

        public string? Direccion { get; set; }

        public string? Referencias { get; set; }
    }
}
